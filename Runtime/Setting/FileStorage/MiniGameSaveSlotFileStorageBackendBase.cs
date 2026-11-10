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

namespace GameFrameX.Setting.Runtime
{
    /// <summary>
    /// 小游戏存档文件存储后端基类。基于各平台 FileSystemManager 的同步接口实现两阶段原子写（写 Temp → 复制到正式区 → 删除 Temp），目录即真相，枚举直接扫根目录；平台差异由派生类以最小原语集提供。
    /// </summary>
    /// <remarks>
    /// Base class for mini-game save slot file storage backends. Implements a two-stage atomic write (write to Temp, copy to the final area, delete Temp) over each platform FileSystemManager's synchronous API; the directory is the source of truth and listing scans the root directory directly. Derived classes supply a minimal set of platform primitives.
    /// </remarks>
    [UnityEngine.Scripting.Preserve]
    public abstract class MiniGameSaveSlotFileStorageBackendBase : ISaveSlotFileStorageBackend
    {
        private const string TempDirectoryName = "Temp";
        private const string RootDirectoryName = "SaveSlotFileStorage";

        /// <summary>
        /// 获取存档文件存储根目录（各平台用户目录下的 <c>SaveSlotFileStorage</c> 子目录）。
        /// </summary>
        /// <remarks>
        /// Gets the save slot file storage root directory (the <c>SaveSlotFileStorage</c> subdirectory under each platform's user directory).
        /// </remarks>
        protected string RootDirectory
        {
            get { return CombinePath(GetPlatformUserDataPath(), RootDirectoryName); }
        }

        /// <summary>
        /// 获取平台用户目录（如微信 <c>wx.env.USER_DATA_PATH</c>）。
        /// </summary>
        /// <remarks>
        /// Gets the platform user directory (for example WeChat <c>wx.env.USER_DATA_PATH</c>).
        /// </remarks>
        /// <returns>平台用户目录路径 / The platform user directory path</returns>
        protected abstract string GetPlatformUserDataPath();

        /// <summary>
        /// 以平台分隔符拼接路径。
        /// </summary>
        /// <remarks>
        /// Combines path segments with the platform separator.
        /// </remarks>
        /// <param name="left">左片段 / The left segment</param>
        /// <param name="right">右片段 / The right segment</param>
        /// <returns>拼接后的路径 / The combined path</returns>
        protected abstract string CombinePath(string left, string right);

        /// <summary>
        /// 同步写入字节文件。
        /// </summary>
        /// <remarks>
        /// Synchronously writes a byte file.
        /// </remarks>
        /// <param name="filePath">目标路径 / The target path</param>
        /// <param name="contents">内容字节 / The contents bytes</param>
        protected abstract void WriteFileBytes(string filePath, byte[] contents);

        /// <summary>
        /// 同步读取字节文件。
        /// </summary>
        /// <remarks>
        /// Synchronously reads a byte file.
        /// </remarks>
        /// <param name="filePath">目标路径 / The target path</param>
        /// <returns>内容字节 / The contents bytes</returns>
        protected abstract byte[] ReadFileBytes(string filePath);

        /// <summary>
        /// 同步复制文件（覆盖已存在目标）。
        /// </summary>
        /// <remarks>
        /// Synchronously copies a file, overwriting an existing target.
        /// </remarks>
        /// <param name="sourceFilePath">源路径 / The source path</param>
        /// <param name="destinationFilePath">目标路径 / The destination path</param>
        protected abstract void CopyFileBytes(string sourceFilePath, string destinationFilePath);

        /// <summary>
        /// 同步删除文件。
        /// </summary>
        /// <remarks>
        /// Synchronously deletes a file.
        /// </remarks>
        /// <param name="filePath">目标路径 / The target path</param>
        protected abstract void DeleteFile(string filePath);

        /// <summary>
        /// 同步检查文件是否存在。
        /// </summary>
        /// <remarks>
        /// Synchronously checks whether a file exists.
        /// </remarks>
        /// <param name="filePath">目标路径 / The target path</param>
        /// <returns>是否存在 / Whether the file exists</returns>
        protected abstract bool ExistsFile(string filePath);

        /// <summary>
        /// 递归创建目录（已存在时不报错）。
        /// </summary>
        /// <remarks>
        /// Creates a directory recursively; does nothing when it already exists.
        /// </remarks>
        /// <param name="directoryPath">目录路径 / The directory path</param>
        protected abstract void CreateDirectoryIfExistsMissing(string directoryPath);

        /// <summary>
        /// 同步列出目录下的直接子项名称（可能混合文件与目录）。目录不存在时应抛出异常或返回 null，由基类归一化为空列表。
        /// </summary>
        /// <remarks>
        /// Synchronously lists the direct child names of a directory (may mix files and directories). When the directory does not exist, throw or return null; the base class normalizes both to an empty list.
        /// </remarks>
        /// <param name="directoryPath">目录路径 / The directory path</param>
        /// <returns>子项名称数组 / Array of child names</returns>
        protected abstract string[] ListDirectoryEntryNames(string directoryPath);

        /// <summary>
        /// 写入存档文件，同名整文件覆盖。两阶段原子写：写 <c>{root}/Temp/{fileName}</c> → 复制到 <c>{root}/{fileName}</c> → 删除 Temp 文件；复制失败时正式区保留旧文件。
        /// </summary>
        /// <remarks>
        /// Writes a save slot file, overwriting any existing file with the same name. Two-stage atomic write: write to <c>{root}/Temp/{fileName}</c>, copy to <c>{root}/{fileName}</c>, then delete the Temp file; when the copy fails, the previous file in the final area is preserved.
        /// </remarks>
        /// <param name="fileName">已通过校验的文件名 / The validated file name</param>
        /// <param name="contents">文件内容字节 / The file contents bytes</param>
        /// <exception cref="SaveSlotFileStorageException">I/O 失败时抛出 / Thrown when an I/O operation fails</exception>
        [UnityEngine.Scripting.Preserve]
        public void Write(string fileName, byte[] contents)
        {
            try
            {
                string rootDirectory = RootDirectory;
                string tempDirectory = CombinePath(rootDirectory, TempDirectoryName);
                CreateDirectoryIfExistsMissing(rootDirectory);
                CreateDirectoryIfExistsMissing(tempDirectory);

                string tempFilePath = CombinePath(tempDirectory, fileName);
                string finalFilePath = CombinePath(rootDirectory, fileName);
                WriteFileBytes(tempFilePath, contents);
                CopyFileBytes(tempFilePath, finalFilePath);
                DeleteFile(tempFilePath);
            }
            catch (Exception exception)
            {
                throw new SaveSlotFileStorageException("Failed to write save slot file '" + fileName + "'.", exception);
            }
        }

        /// <summary>
        /// 读取存档文件。只读正式区，<c>Temp</c> 目录内的文件永不对外可见。
        /// </summary>
        /// <remarks>
        /// Reads a save slot file. Only the final area is read; files inside the <c>Temp</c> directory are never visible to callers.
        /// </remarks>
        /// <param name="fileName">已通过校验的文件名 / The validated file name</param>
        /// <returns>文件内容字节 / The file contents bytes</returns>
        /// <exception cref="SaveSlotFileStorageFileNotFoundException">文件不存在时抛出 / Thrown when the file does not exist</exception>
        /// <exception cref="SaveSlotFileStorageException">I/O 失败时抛出 / Thrown when an I/O operation fails</exception>
        [UnityEngine.Scripting.Preserve]
        public byte[] Read(string fileName)
        {
            string finalFilePath = CombinePath(RootDirectory, fileName);
            if (!ExistsFile(finalFilePath))
            {
                throw new SaveSlotFileStorageFileNotFoundException(fileName);
            }

            try
            {
                return ReadFileBytes(finalFilePath);
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
            return ExistsFile(CombinePath(RootDirectory, fileName));
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
            string finalFilePath = CombinePath(RootDirectory, fileName);
            if (!ExistsFile(finalFilePath))
            {
                return false;
            }

            try
            {
                DeleteFile(finalFilePath);
                return true;
            }
            catch (Exception exception)
            {
                throw new SaveSlotFileStorageException("Failed to delete save slot file '" + fileName + "'.", exception);
            }
        }

        /// <summary>
        /// 列出根目录下全部存档文件名。目录即真相：直接扫描根目录，不维护索引文件；<c>Temp</c> 目录与不合规文件名被排除。目录不存在时返回空数组，永不抛出。
        /// </summary>
        /// <remarks>
        /// Lists all save slot file names under the root directory. The directory is the source of truth: the root directory is scanned directly without an index file; the <c>Temp</c> directory and invalid file names are excluded. Returns an empty array when the directory does not exist; never throws.
        /// </remarks>
        /// <returns>文件名数组 / Array of file names</returns>
        [UnityEngine.Scripting.Preserve]
        public string[] ListFiles()
        {
            List<string> fileNames = new List<string>();
            try
            {
                string rootDirectory = RootDirectory;
                string[] entryNames = ListDirectoryEntryNames(rootDirectory);
                if (entryNames == null)
                {
                    return new string[0];
                }

                foreach (string entryName in entryNames)
                {
                    if (entryName == TempDirectoryName)
                    {
                        continue;
                    }

                    if (!SaveSlotFileStorageNameValidator.IsValid(entryName))
                    {
                        continue;
                    }

                    if (!ExistsFile(CombinePath(rootDirectory, entryName)))
                    {
                        continue;
                    }

                    fileNames.Add(entryName);
                }
            }
            catch (Exception)
            {
                // 目录不存在等平台侧报错视为空列表
                return new string[0];
            }

            return fileNames.ToArray();
        }
    }
}
