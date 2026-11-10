using System;
using System.Collections.Generic;
using System.IO;
using GameFrameX.Runtime;
using GameFrameX.Setting.Runtime;
using NUnit.Framework;
using UnityEngine;

namespace GameFrameX.Setting.Tests
{
    internal sealed class SaveSlotFileStorageTests
    {
        private string m_RootDirectory;

        [SetUp]
        public void Setup()
        {
            m_RootDirectory = Path.Combine(Application.temporaryCachePath, "SaveSlotFileStorageTests-" + Guid.NewGuid().ToString("N"));
            SaveSlotFileStorageBackendFactory.ResetForTests();
            SaveSlotFileStorageBackendFactory.SetSaveSlotFileStorageRoot(m_RootDirectory);
        }

        [TearDown]
        public void TearDown()
        {
            SaveSlotFileStorageBackendFactory.ResetForTests();
            if (Directory.Exists(m_RootDirectory))
            {
                Directory.Delete(m_RootDirectory, true);
            }
        }

        private LocalDiskSaveSlotFileStorageBackend CreateBackend()
        {
            return new LocalDiskSaveSlotFileStorageBackend(m_RootDirectory);
        }

        [Test]
        public void NameValidator_RejectsIllegalNames()
        {
            Assert.That(SaveSlotFileStorageNameValidator.IsValid("profile1_battle.dat"), Is.True);
            Assert.That(SaveSlotFileStorageNameValidator.IsValid("save-01.json"), Is.True);
            Assert.That(SaveSlotFileStorageNameValidator.IsValid(null), Is.False);
            Assert.That(SaveSlotFileStorageNameValidator.IsValid(string.Empty), Is.False);
            Assert.That(SaveSlotFileStorageNameValidator.IsValid("../escape.sav"), Is.False);
            Assert.That(SaveSlotFileStorageNameValidator.IsValid("a/b.dat"), Is.False);
            Assert.That(SaveSlotFileStorageNameValidator.IsValid("a b.dat"), Is.False);
            Assert.That(SaveSlotFileStorageNameValidator.IsValid(new string('a', 97)), Is.False);
            Assert.That(SaveSlotFileStorageNameValidator.IsValid(new string('a', 96)), Is.True);
            Assert.Throws<SaveSlotFileStorageNameInvalidException>(() => SaveSlotFileStorageNameValidator.EnsureValid("a/b.dat"));
        }

        [Test]
        public void Backend_WriteReadRoundTrip_OverwritesSameName()
        {
            LocalDiskSaveSlotFileStorageBackend backend = CreateBackend();
            byte[] first = new byte[] { 1, 2, 3 };
            byte[] second = new byte[] { 9 };

            backend.Write("slot.dat", first);
            Assert.That(backend.Read("slot.dat"), Is.EqualTo(first));

            backend.Write("slot.dat", second);
            Assert.That(backend.Read("slot.dat"), Is.EqualTo(second));
            Assert.That(backend.Exists("slot.dat"), Is.True);

            // 两阶段写完成后 Temp 目录为空，正式区可见文件
            string tempDirectory = Path.Combine(m_RootDirectory, "Temp");
            Assert.That(Directory.Exists(tempDirectory), Is.True);
            Assert.That(Directory.GetFiles(tempDirectory).Length, Is.EqualTo(0));
            Assert.That(Directory.GetFiles(m_RootDirectory, "slot.dat").Length, Is.EqualTo(1));
        }

        [Test]
        public void Backend_ReadMissingFile_ThrowsNotFound()
        {
            LocalDiskSaveSlotFileStorageBackend backend = CreateBackend();

            Assert.Throws<SaveSlotFileStorageFileNotFoundException>(() => backend.Read("missing.dat"));
        }

        [Test]
        public void Backend_DeleteReportsExistence_ListsFilesOnlyInRoot()
        {
            LocalDiskSaveSlotFileStorageBackend backend = CreateBackend();

            Assert.That(backend.Delete("slot.dat"), Is.False);

            backend.Write("alpha.dat", new byte[] { 1 });
            backend.Write("beta.sav", new byte[] { 2, 3 });

            List<string> fileNames = new List<string>(backend.ListFiles());
            Assert.That(fileNames, Is.EquivalentTo(new string[] { "alpha.dat", "beta.sav" }));

            Assert.That(backend.Delete("alpha.dat"), Is.True);
            Assert.That(backend.Exists("alpha.dat"), Is.False);
            Assert.That(backend.Delete("alpha.dat"), Is.False);
            Assert.That(backend.ListFiles(), Is.EquivalentTo(new string[] { "beta.sav" }));
        }

        [Test]
        public void Backend_ListFiles_MissingDirectoryReturnsEmpty()
        {
            LocalDiskSaveSlotFileStorageBackend backend = new LocalDiskSaveSlotFileStorageBackend(Path.Combine(m_RootDirectory, "not-created"));

            Assert.That(backend.ListFiles(), Is.Empty);
        }

        [Test]
        public void Factory_ChannelRegistrationKeepsFirst()
        {
            SaveSlotFileStorageBackendFactory.ResetForTests();
            SaveSlotFileStorageBackendFactory.RegisterChannelBackend(() => new LocalDiskSaveSlotFileStorageBackend(m_RootDirectory));
            SaveSlotFileStorageBackendFactory.RegisterChannelBackend(() => new LocalDiskSaveSlotFileStorageBackend(Path.Combine(m_RootDirectory, "other")));

            ISaveSlotFileStorageBackend backend = SaveSlotFileStorageBackendFactory.Create();
            backend.Write("channel.dat", new byte[] { 7 });
            Assert.That(File.Exists(Path.Combine(m_RootDirectory, "channel.dat")), Is.True);
        }
    }
}
