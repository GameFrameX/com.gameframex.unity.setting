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
    /// 存档文件存储读写失败的基异常。
    /// </summary>
    /// <remarks>
    /// Base exception thrown when a save slot file storage operation fails.
    /// </remarks>
    [UnityEngine.Scripting.Preserve]
    public class SaveSlotFileStorageException : Exception
    {
        /// <summary>
        /// 初始化存档文件存储异常。
        /// </summary>
        /// <remarks>
        /// Initializes the save slot file storage exception.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public SaveSlotFileStorageException()
        {
        }

        /// <summary>
        /// 使用指定错误消息初始化存档文件存储异常。
        /// </summary>
        /// <remarks>
        /// Initializes the exception with the specified error message.
        /// </remarks>
        /// <param name="message">错误消息 / The error message</param>
        [UnityEngine.Scripting.Preserve]
        public SaveSlotFileStorageException(string message) : base(message)
        {
        }

        /// <summary>
        /// 使用指定错误消息与内部异常初始化存档文件存储异常。
        /// </summary>
        /// <remarks>
        /// Initializes the exception with the specified error message and inner exception.
        /// </remarks>
        /// <param name="message">错误消息 / The error message</param>
        /// <param name="innerException">内部异常 / The inner exception</param>
        [UnityEngine.Scripting.Preserve]
        public SaveSlotFileStorageException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
