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

namespace GameFrameX.Setting.Runtime
{
    /// <summary>
    /// 游戏配置组件的存档文件存储分部：按单一 <c>fileName</c> 的整文件读写通道，与键值配置通道互不干扰。
    /// <remarks>
    /// Save slot file storage partial of the game setting component: a whole-file read/write channel addressed by a single file name, independent of the key-value settings channel.
    /// </remarks>
    public sealed partial class SettingComponent
    {
        private ISaveSlotFileStorageBackend m_SaveSlotFileStorageBackend = null;

        private void EnsureSaveSlotFileStorageBackend()
        {
            if (m_SaveSlotFileStorageBackend == null)
            {
                m_SaveSlotFileStorageBackend = SaveSlotFileStorageBackendFactory.Create();
            }
        }

        /// <summary>
        /// 写入存档文件存储中的整文件，同名覆盖（两阶段原子写）。
        /// </summary>
        /// <remarks>
        /// Writes a whole file into the save slot file storage, overwriting any file with the same name (two-stage atomic write).
        /// </remarks>
        /// <param name="fileName">存档文件名（仅允许 A-Z a-z 0-9 . _ - ，长度 1-96）/ The save slot file name (only A-Z a-z 0-9 . _ - allowed, length 1-96)</param>
        /// <param name="contents">文件内容字节 / The file contents bytes</param>
        /// <exception cref="SaveSlotFileStorageNameInvalidException">文件名不合规时抛出 / Thrown when the file name is invalid</exception>
        /// <exception cref="SaveSlotFileStorageException">I/O 失败时抛出 / Thrown when an I/O operation fails</exception>
        [UnityEngine.Scripting.Preserve]
        public void WriteFileStorage(string fileName, byte[] contents)
        {
            EnsureSaveSlotFileStorageBackend();
            SaveSlotFileStorageNameValidator.EnsureValid(fileName);
            m_SaveSlotFileStorageBackend.Write(fileName, contents);
        }

        /// <summary>
        /// 读取存档文件存储中的整文件。
        /// </summary>
        /// <remarks>
        /// Reads a whole file from the save slot file storage.
        /// </remarks>
        /// <param name="fileName">存档文件名 / The save slot file name</param>
        /// <returns>文件内容字节 / The file contents bytes</returns>
        /// <exception cref="SaveSlotFileStorageNameInvalidException">文件名不合规时抛出 / Thrown when the file name is invalid</exception>
        /// <exception cref="SaveSlotFileStorageFileNotFoundException">文件不存在时抛出 / Thrown when the file does not exist</exception>
        /// <exception cref="SaveSlotFileStorageException">I/O 失败时抛出 / Thrown when an I/O operation fails</exception>
        [UnityEngine.Scripting.Preserve]
        public byte[] ReadFileStorage(string fileName)
        {
            EnsureSaveSlotFileStorageBackend();
            SaveSlotFileStorageNameValidator.EnsureValid(fileName);
            return m_SaveSlotFileStorageBackend.Read(fileName);
        }

        /// <summary>
        /// 检查存档文件是否存在。
        /// </summary>
        /// <remarks>
        /// Checks whether a save slot file exists.
        /// </remarks>
        /// <param name="fileName">存档文件名 / The save slot file name</param>
        /// <returns>是否存在 / Whether the file exists</returns>
        /// <exception cref="SaveSlotFileStorageNameInvalidException">文件名不合规时抛出 / Thrown when the file name is invalid</exception>
        [UnityEngine.Scripting.Preserve]
        public bool ExistsInFileStorage(string fileName)
        {
            EnsureSaveSlotFileStorageBackend();
            SaveSlotFileStorageNameValidator.EnsureValid(fileName);
            return m_SaveSlotFileStorageBackend.Exists(fileName);
        }

        /// <summary>
        /// 删除存档文件。文件不存在返回 <c>false</c>。
        /// </summary>
        /// <remarks>
        /// Deletes a save slot file. Returns <c>false</c> when the file does not exist.
        /// </remarks>
        /// <param name="fileName">存档文件名 / The save slot file name</param>
        /// <returns>文件存在并删除返回 <c>true</c>；不存在返回 <c>false</c> / <c>true</c> when the file existed and was deleted; <c>false</c> otherwise</returns>
        /// <exception cref="SaveSlotFileStorageNameInvalidException">文件名不合规时抛出 / Thrown when the file name is invalid</exception>
        /// <exception cref="SaveSlotFileStorageException">I/O 失败时抛出 / Thrown when an I/O operation fails</exception>
        [UnityEngine.Scripting.Preserve]
        public bool DeleteFromFileStorage(string fileName)
        {
            EnsureSaveSlotFileStorageBackend();
            SaveSlotFileStorageNameValidator.EnsureValid(fileName);
            return m_SaveSlotFileStorageBackend.Delete(fileName);
        }

        /// <summary>
        /// 列出存档文件存储中的全部文件名。目录不存在时返回空数组，永不抛出。
        /// </summary>
        /// <remarks>
        /// Lists all file names in the save slot file storage. Returns an empty array when the directory does not exist; never throws.
        /// </remarks>
        /// <returns>文件名数组 / Array of file names</returns>
        [UnityEngine.Scripting.Preserve]
        public string[] ListFilesInFileStorage()
        {
            EnsureSaveSlotFileStorageBackend();
            return m_SaveSlotFileStorageBackend.ListFiles();
        }
    }
}
