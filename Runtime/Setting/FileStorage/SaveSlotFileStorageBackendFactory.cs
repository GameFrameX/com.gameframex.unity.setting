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
using GameFrameX.Runtime;
using UnityEngine;

namespace GameFrameX.Setting.Runtime
{
    /// <summary>
    /// 创建当前运行环境使用的存档文件存储后端。默认返回本地磁盘后端（根目录为 <c>{persistentDataPath}/SaveSlotFileStorage/</c>）；渠道包可通过 <see cref="RegisterChannelBackend"/> 注册平台后端覆盖。
    /// </summary>
    /// <remarks>
    /// Creates the save slot file storage backend for the current runtime environment. Defaults to the local-disk backend rooted at <c>{persistentDataPath}/SaveSlotFileStorage/</c>; channel packages may register their platform backend via <see cref="RegisterChannelBackend"/> to override.
    /// </remarks>
    [UnityEngine.Scripting.Preserve]
    public static class SaveSlotFileStorageBackendFactory
    {
        private static Func<ISaveSlotFileStorageBackend> s_ChannelBackendFactory;

        private static string s_RootDirectory;

        /// <summary>
        /// 获取存档文件存储根目录。未设置时默认 <c>{Application.persistentDataPath}/SaveSlotFileStorage</c>。
        /// </summary>
        /// <remarks>
        /// Gets the save slot file storage root directory. Defaults to <c>{Application.persistentDataPath}/SaveSlotFileStorage</c> when unset.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public static string SaveSlotFileStorageRoot
        {
            get
            {
                if (s_RootDirectory == null)
                {
                    s_RootDirectory = PathHelper.Combine(Application.persistentDataPath, "SaveSlotFileStorage");
                }

                return s_RootDirectory;
            }
        }

        /// <summary>
        /// 设置存档文件存储根目录（启动期配置）。必须在首次创建后端前调用；重复调用保留首个并输出 Warning。
        /// </summary>
        /// <remarks>
        /// Sets the save slot file storage root directory (startup-time configuration). Must be called before the first backend creation; duplicate calls keep the first and log a warning.
        /// </remarks>
        /// <param name="rootDirectory">根目录 / The root directory</param>
        [UnityEngine.Scripting.Preserve]
        public static void SetSaveSlotFileStorageRoot(string rootDirectory)
        {
            if (string.IsNullOrEmpty(rootDirectory))
            {
                throw new SaveSlotFileStorageException("Save slot file storage root directory is invalid.");
            }

            if (s_RootDirectory != null)
            {
                Log.Warning("Save slot file storage root is already configured. The duplicate configuration is ignored.");
                return;
            }

            s_RootDirectory = rootDirectory;
        }

        /// <summary>
        /// 注册渠道存档文件存储后端工厂。渠道包通过 <c>RuntimeInitializeOnLoadMethod(SubsystemRegistration)</c> 自注册（引用即注册），注册时机早于 <see cref="SettingComponent"/> 首次创建后端。重复注册保留首个并输出 Warning。
        /// </summary>
        /// <remarks>
        /// Registers a channel save slot file storage backend factory. Channel packages self-register via <c>RuntimeInitializeOnLoadMethod(SubsystemRegistration)</c>, before <see cref="SettingComponent"/> first creates a backend. Duplicate registrations keep the first and log a warning.
        /// </remarks>
        /// <param name="backendFactory">后端工厂，返回平台存储后端实例 / The backend factory returning a platform storage backend instance</param>
        [UnityEngine.Scripting.Preserve]
        public static void RegisterChannelBackend(Func<ISaveSlotFileStorageBackend> backendFactory)
        {
            if (backendFactory == null)
            {
                throw new SaveSlotFileStorageException("Storage backend factory is invalid.");
            }

            if (s_ChannelBackendFactory != null)
            {
                Log.Warning("Channel save slot file storage backend is already registered. The duplicate registration is ignored.");
                return;
            }

            s_ChannelBackendFactory = backendFactory;
        }

        /// <summary>
        /// 创建当前运行环境使用的存档文件存储后端。优先取渠道注册，未注册时返回 <see cref="LocalDiskSaveSlotFileStorageBackend"/>。
        /// </summary>
        /// <remarks>
        /// Creates the save slot file storage backend for the current runtime environment. Prefers the channel registration; returns a <see cref="LocalDiskSaveSlotFileStorageBackend"/> when nothing is registered.
        /// </remarks>
        /// <returns>存档文件存储后端实例 / The save slot file storage backend instance</returns>
        [UnityEngine.Scripting.Preserve]
        public static ISaveSlotFileStorageBackend Create()
        {
            return s_ChannelBackendFactory == null ? new LocalDiskSaveSlotFileStorageBackend(SaveSlotFileStorageRoot) : s_ChannelBackendFactory();
        }

        /// <summary>
        /// 重置渠道后端注册与根目录配置。仅供单元测试在用例间清理注册状态使用。
        /// </summary>
        /// <remarks>
        /// Resets the channel backend registration and root directory configuration. Test-only hook for cleaning registration state between test cases.
        /// </remarks>
        internal static void ResetForTests()
        {
            s_ChannelBackendFactory = null;
            s_RootDirectory = null;
        }
    }
}
