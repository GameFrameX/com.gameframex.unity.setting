// ==========================================================================================
//   GameFrameX 组织及其衍生项目的版权、商标、专利及其他相关权利
//   GameFrameX organization and its derivative projects' copyrights, trademarks, patents, and related rights
//   均受中华人民共和国及相关国际法律法规保护。
//   are protected by the laws of the People's Republic of China and relevant international regulations.
//   使用本项目须严格遵守相应法律法规与开源许可证之规定。
//   Usage of this project must strictly comply with applicable laws, regulations, and open-source licenses.
//   本项目采用 MIT 许可证与 Apache License 2.0 双许可证分发，
//   This project is dual-licensed under the MIT License and Apache License 2.0,
//   complete license text please see the LICENSE file in the source code root directory.
//   禁止利用本项目实施任何危害国家安全、破坏社会秩序、
//   It is prohibited to use this project to engage in any activities that endanger national security, disrupt social order,
//   侵犯他人合法权益等法律法规所禁止的行为！
//   or infringe any legal rights and interests of others as prohibited by laws and regulations!
//   因基于本项目二次开发所产生的一切法律纠纷与责任，
//   Any legal disputes and liabilities arising from secondary development based on this project
//   shall be borne solely by the developer of the project; the project organization and contributors assume no responsibility.
//   GitHub 仓库：https://github.com/GameFrameX
//   GitHub Repository: https://github.com/GameFrameX
//   Gitee  仓库：https://gitee.com/GameFrameX
//   Gitee Repository:  https://gitee.com/GameFrameX
//   CNB 仓库：https://cnb.cool/GameFrameX
//   CNB Repository:  https://cnb.cool/GameFrameX
//   官方文档：https://gameframex.doc.alianblank.com/
//   Official Documentation: https://gameframex.doc.alianblank.com/
//  ==========================================================================================

using System;
using System.Collections.Generic;
using GameFrameX.Runtime;

namespace GameFrameX.Setting.Runtime
{
    /// <summary>
    /// 本地磁盘存档文件存储后端。目录即真相：写入经 <c>Temp</c> 目录两阶段原子写，枚举直接扫描根目录；全部 I/O 经 <see cref="FileHelper"/> 与 <see cref="PathHelper"/>，不直接调用 <c>System.IO</c> 文件 API。
    /// </summary>
    /// <remarks>
    /// Local-disk save slot file storage backend. The directory is the source of truth: writes go through a two-stage atomic write in a <c>Temp</c> directory, and listing scans the root directory directly; all I/O goes through <see cref="FileHelper"/> and <see cref="PathHelper"/> without direct <c>System.IO</c> file API calls.
    /// </remarks>
    [UnityEngine.Scripting.Preserve]
    public sealed class LocalDiskSaveSlotFileStorageBackend : ISaveSlotFileStorageBackend
    {
        private const string TempDirectoryName = "Temp";

        private readonly string m_RootDirectory;

        /// <summary>
        /// 使用指定根目录初始化后端。
        /// </summary>
        /// <remarks>
        /// Initializes the backend with the specified root directory.
        /// </remarks>
        /// <param name="rootDirectory">根目录（应位于可读写目录，如 <c>Application.persistentDataPath</c> 之下）/ The root directory (should be inside a writable location such as <c>Application.persistentDataPath</c>)</param>
        [UnityEngine.Scripting.Preserve]
        public LocalDiskSaveSlotFileStorageBackend(string rootDirectory)
        {
            if (string.IsNullOrEmpty(rootDirectory))
            {
                throw new SaveSlotFileStorageException("Save slot file storage root directory is invalid.");
            }

            m_RootDirectory = rootDirectory;
        }

        /// <summary>
        /// 写入存档文件，同名整文件覆盖。
        /// </summary>
        /// <remarks>
        /// 两阶段原子写：先写 <c>{root}/Temp/{fileName}</c>，再移动到 <c>{root}/{fileName}</c>；每次写入前清空 Temp。移动失败时正式区保留旧文件。
        /// Writes a save slot file, overwriting any existing file with the same name.
        /// Two-stage atomic write: writes to <c>{root}/Temp/{fileName}</c> first, then moves it to <c>{root}/{fileName}</c>; Temp is cleared before each write. When the move fails, the previous file in the final area is preserved.
        /// </remarks>
        /// <param name="fileName">已通过校验的文件名 / The validated file name</param>
        /// <param name="contents">文件内容字节 / The file contents bytes</param>
        /// <exception cref="SaveSlotFileStorageException">I/O 失败时抛出 / Thrown when an I/O operation fails</exception>
        [UnityEngine.Scripting.Preserve]
        public void Write(string fileName, byte[] contents)
        {
            try
            {
                string tempDirectory = PathHelper.Combine(m_RootDirectory, TempDirectoryName);
                PathHelper.CreateIfNotExists(m_RootDirectory);
                PathHelper.CreateIfNotExists(tempDirectory);
                FileHelper.CleanDirectory(tempDirectory);

                string tempFilePath = PathHelper.Combine(tempDirectory, fileName);
                string finalFilePath = PathHelper.Combine(m_RootDirectory, fileName);
                FileHelper.WriteAllBytes(tempFilePath, contents);
                FileHelper.Move(tempFilePath, finalFilePath);
            }
            catch (Exception exception)
            {
                throw new SaveSlotFileStorageException("Failed to write save slot file '" + fileName + "'.", exception);
            }
        }

        /// <summary>
        /// 读取存档文件。
        /// </summary>
        /// <remarks>
        /// 只读正式区，<c>Temp</c> 目录内的文件永不对外可见。
        /// Reads a save slot file. Only the final area is read; files inside the <c>Temp</c> directory are never visible to callers.
        /// </remarks>
        /// <param name="fileName">已通过校验的文件名 / The validated file name</param>
        /// <returns>文件内容字节 / The file contents bytes</returns>
        /// <exception cref="SaveSlotFileStorageFileNotFoundException">文件不存在时抛出 / Thrown when the file does not exist</exception>
        /// <exception cref="SaveSlotFileStorageException">I/O 失败时抛出 / Thrown when an I/O operation fails</exception>
        [UnityEngine.Scripting.Preserve]
        public byte[] Read(string fileName)
        {
            string finalFilePath = PathHelper.Combine(m_RootDirectory, fileName);
            if (!FileHelper.IsExists(finalFilePath))
            {
                throw new SaveSlotFileStorageFileNotFoundException(fileName);
            }

            try
            {
                return FileHelper.ReadAllBytes(finalFilePath);
            }
            catch (Exception exception)
            {
                throw new SaveSlotFileStorageException("Failed to read save slot file '" + fileName + "'.", exception);
            }
        }

        /// <summary>
        /// 检查存档文件是否存在。
        /// </summary>
        /// <remarks>
        /// Checks whether a save slot file exists in the final area.
        /// </remarks>
        /// <param name="fileName">已通过校验的文件名 / The validated file name</param>
        /// <returns>是否存在 / Whether the file exists</returns>
        [UnityEngine.Scripting.Preserve]
        public bool Exists(string fileName)
        {
            return FileHelper.IsExists(PathHelper.Combine(m_RootDirectory, fileName));
        }

        /// <summary>
        /// 删除存档文件。
        /// </summary>
        /// <remarks>
        /// Deletes a save slot file from the final area.
        /// </remarks>
        /// <param name="fileName">已通过校验的文件名 / The validated file name</param>
        /// <returns>文件存在并删除返回 <c>true</c>；不存在返回 <c>false</c> / <c>true</c> when the file existed and was deleted; <c>false</c> when it did not exist</returns>
        /// <exception cref="SaveSlotFileStorageException">I/O 失败时抛出 / Thrown when an I/O operation fails</exception>
        [UnityEngine.Scripting.Preserve]
        public bool Delete(string fileName)
        {
            string finalFilePath = PathHelper.Combine(m_RootDirectory, fileName);
            if (!FileHelper.IsExists(finalFilePath))
            {
                return false;
            }

            try
            {
                FileHelper.Delete(finalFilePath);
                return true;
            }
            catch (Exception exception)
            {
                throw new SaveSlotFileStorageException("Failed to delete save slot file '" + fileName + "'.", exception);
            }
        }

        /// <summary>
        /// 列出根目录下全部存档文件名。
        /// </summary>
        /// <remarks>
        /// 目录即真相：直接扫描根目录，不维护索引文件；<c>Temp</c> 目录与不合规文件名被排除。目录不存在时返回空数组，永不抛出。
        /// Lists all save slot file names under the root directory. The directory is the source of truth: the root directory is scanned directly without an index file; the <c>Temp</c> directory and invalid file names are excluded. Returns an empty array when the directory does not exist; never throws.
        /// </remarks>
        /// <returns>文件名数组 / Array of file names</returns>
        /// <exception cref="SaveSlotFileStorageException">扫描目录失败时抛出 / Thrown when scanning the directory fails</exception>
        [UnityEngine.Scripting.Preserve]
        public string[] ListFiles()
        {
            List<string> filePaths = new List<string>();
            try
            {
                FileHelper.GetAllFiles(filePaths, m_RootDirectory);
            }
            catch (Exception exception)
            {
                throw new SaveSlotFileStorageException("Failed to list save slot files.", exception);
            }

            List<string> fileNames = new List<string>();
            string tempPrefix = PathHelper.Combine(m_RootDirectory, TempDirectoryName) + "/";
            foreach (string filePath in filePaths)
            {
                string normalizedPath = PathHelper.NormalizePath(filePath);
                if (normalizedPath != null && normalizedPath.StartsWith(tempPrefix, StringComparison.Ordinal))
                {
                    continue;
                }

                int lastSeparatorIndex = normalizedPath == null ? -1 : normalizedPath.LastIndexOf('/');
                string fileName = lastSeparatorIndex >= 0 ? normalizedPath.Substring(lastSeparatorIndex + 1) : normalizedPath;
                if (!SaveSlotFileStorageNameValidator.IsValid(fileName))
                {
                    continue;
                }

                fileNames.Add(fileName);
            }

            return fileNames.ToArray();
        }
    }
}
