using System;
using System.Collections.Generic;
using GameFrameX.Setting.Runtime;
using NUnit.Framework;

namespace GameFrameX.Setting.Tests
{
    internal sealed class MiniGameSaveSlotFileStorageBackendBaseTests
    {
        /// <summary>
        /// 内存版平台原语，用于验证基类的两阶段写、Temp 隔离与列表过滤编排。
        /// </summary>
        private sealed class InMemoryMiniGameBackend : MiniGameSaveSlotFileStorageBackendBase
        {
            private readonly Dictionary<string, byte[]> m_Files = new Dictionary<string, byte[]>();
            private readonly HashSet<string> m_Directories = new HashSet<string>();

            public int CopyFileCallCount { get; private set; }

            protected override string GetPlatformUserDataPath()
            {
                return "userroot";
            }

            protected override string CombinePath(string left, string right)
            {
                return left + "/" + right;
            }

            protected override void WriteFileBytes(string filePath, byte[] contents)
            {
                m_Files[filePath] = contents;
            }

            protected override byte[] ReadFileBytes(string filePath)
            {
                return m_Files[filePath];
            }

            protected override void CopyFileBytes(string sourceFilePath, string destinationFilePath)
            {
                CopyFileCallCount++;
                m_Files[destinationFilePath] = m_Files[sourceFilePath];
            }

            protected override void DeleteFile(string filePath)
            {
                m_Files.Remove(filePath);
            }

            protected override bool ExistsFile(string filePath)
            {
                return m_Files.ContainsKey(filePath) || m_Directories.Contains(filePath);
            }

            protected override void CreateDirectoryIfExistsMissing(string directoryPath)
            {
                m_Directories.Add(directoryPath);
            }

            protected override string[] ListDirectoryEntryNames(string directoryPath)
            {
                if (!m_Directories.Contains(directoryPath))
                {
                    throw new InvalidOperationException("directory not found");
                }

                HashSet<string> entryNames = new HashSet<string>();
                string prefix = directoryPath + "/";
                foreach (string filePath in m_Files.Keys)
                {
                    if (filePath.StartsWith(prefix, StringComparison.Ordinal))
                    {
                        entryNames.Add(filePath.Substring(prefix.Length));
                    }
                }

                foreach (string directoryPathEntry in m_Directories)
                {
                    if (directoryPathEntry.StartsWith(prefix, StringComparison.Ordinal) && directoryPathEntry.IndexOf('/', prefix.Length) < 0)
                    {
                        entryNames.Add(directoryPathEntry.Substring(prefix.Length));
                    }
                }

                string[] names = new string[entryNames.Count];
                entryNames.CopyTo(names);
                return names;
            }
        }

        [Test]
        public void BackendBase_WriteReadRoundTrip_OverwritesSameNameAndClearsTemp()
        {
            InMemoryMiniGameBackend backend = new InMemoryMiniGameBackend();
            byte[] first = new byte[] { 1, 2, 3 };
            byte[] second = new byte[] { 9 };

            backend.Write("slot.dat", first);
            Assert.That(backend.Read("slot.dat"), Is.EqualTo(first));

            backend.Write("slot.dat", second);
            Assert.That(backend.Read("slot.dat"), Is.EqualTo(second));
            Assert.That(backend.CopyFileCallCount, Is.EqualTo(2));

            // Temp 文件在写入完成后被删除，永不残留、永不外显
            Assert.That(backend.Exists("Temp/slot.dat"), Is.False);
            Assert.That(backend.ListFiles(), Is.EquivalentTo(new string[] { "slot.dat" }));
        }

        [Test]
        public void BackendBase_ReadMissingFile_ThrowsNotFound()
        {
            InMemoryMiniGameBackend backend = new InMemoryMiniGameBackend();

            Assert.Throws<SaveSlotFileStorageFileNotFoundException>(() => backend.Read("missing.dat"));
        }

        [Test]
        public void BackendBase_DeleteReportsExistence_ListsOnlyValidEntries()
        {
            InMemoryMiniGameBackend backend = new InMemoryMiniGameBackend();

            Assert.That(backend.Delete("slot.dat"), Is.False);

            backend.Write("alpha.dat", new byte[] { 1 });
            Assert.That(backend.ListFiles(), Is.EquivalentTo(new string[] { "alpha.dat" }));
            Assert.That(backend.Delete("alpha.dat"), Is.True);
            Assert.That(backend.Exists("alpha.dat"), Is.False);
        }

        [Test]
        public void BackendBase_ListFiles_MissingDirectoryReturnsEmptyWithoutThrowing()
        {
            InMemoryMiniGameBackend backend = new InMemoryMiniGameBackend();

            Assert.That(backend.ListFiles(), Is.Empty);
        }
    }
}
