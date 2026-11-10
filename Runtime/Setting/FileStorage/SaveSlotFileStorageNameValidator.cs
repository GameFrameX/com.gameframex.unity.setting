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

namespace GameFrameX.Setting.Runtime
{
    /// <summary>
    /// 存档文件名校验器。只允许字母、数字与 <c>. _ -</c>，长度 1-96，禁止路径分隔符、<c>..</c> 与空白，收敛小游戏文件系统兼容面。
    /// </summary>
    /// <remarks>
    /// Validates save slot file names. Only letters, digits and <c>. _ -</c> are allowed, with length 1-96; path separators, <c>..</c> and whitespace are rejected to keep the mini-game file system compatibility surface narrow.
    /// </remarks>
    [UnityEngine.Scripting.Preserve]
    public static class SaveSlotFileStorageNameValidator
    {
        private const int MinimumFileNameLength = 1;
        private const int MaximumFileNameLength = 96;

        /// <summary>
        /// 校验文件名是否合规。
        /// </summary>
        /// <remarks>
        /// Checks whether the file name is valid.
        /// </remarks>
        /// <param name="fileName">待校验的文件名 / The file name to validate</param>
        /// <returns>合规返回 <c>true</c>；为 null、长度越界或含非法字符返回 <c>false</c> / <c>true</c> when valid; <c>false</c> when null, out of length bounds, or containing illegal characters</returns>
        [UnityEngine.Scripting.Preserve]
        public static bool IsValid(string fileName)
        {
            if (fileName == null || fileName.Length < MinimumFileNameLength || fileName.Length > MaximumFileNameLength)
            {
                return false;
            }

            if (fileName.IndexOf("..", StringComparison.Ordinal) >= 0)
            {
                return false;
            }

            for (int index = 0; index < fileName.Length; index++)
            {
                char character = fileName[index];
                bool allowed = (character >= 'A' && character <= 'Z')
                               || (character >= 'a' && character <= 'z')
                               || (character >= '0' && character <= '9')
                               || character == '.' || character == '_' || character == '-';
                if (!allowed)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 校验文件名，不合规时抛出 <see cref="SaveSlotFileStorageNameInvalidException"/>。
        /// </summary>
        /// <remarks>
        /// Validates the file name and throws <see cref="SaveSlotFileStorageNameInvalidException"/> when invalid.
        /// </remarks>
        /// <param name="fileName">待校验的文件名 / The file name to validate</param>
        [UnityEngine.Scripting.Preserve]
        public static void EnsureValid(string fileName)
        {
            if (!IsValid(fileName))
            {
                throw new SaveSlotFileStorageNameInvalidException(fileName);
            }
        }
    }
}
