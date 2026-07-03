using System;
using GameFrameX.Runtime;

namespace GameFrameX.Setting.Runtime
{
    /// <summary>
    /// 创建当前运行环境使用的配置存储后端。
    /// </summary>
    /// <remarks>
    /// Creates the setting storage backend for the current runtime environment.
    /// </remarks>
    internal static class SettingStorageBackendFactory
    {
        private static Func<ISettingStorageBackend> s_MiniGameBackendFactory;

        /// <summary>
        /// 注册小游戏平台配置存储后端工厂。平台 adapter assembly 通过直接 SDK 调用实现后端，再在启动阶段注册到 common setting assembly。
        /// </summary>
        /// <remarks>
        /// Registers a mini-game setting storage backend factory. Platform adapter assemblies implement direct SDK calls and register here during startup.
        /// </remarks>
        internal static void RegisterMiniGameBackend(Func<ISettingStorageBackend> backendFactory)
        {
            if (backendFactory == null)
            {
                throw new GameFrameworkException("Storage backend factory is invalid.");
            }

            s_MiniGameBackendFactory = backendFactory;
        }

        internal static ISettingStorageBackend Create()
        {
#if UNITY_EDITOR
            return new UnityPlayerPrefsSettingStorage();
#else
            return s_MiniGameBackendFactory == null ? new UnityPlayerPrefsSettingStorage() : s_MiniGameBackendFactory();
#endif
        }

        internal static void ResetMiniGameBackendForTests()
        {
            s_MiniGameBackendFactory = null;
        }
    }
}
