namespace GameFrameX.Setting.Runtime
{
    internal sealed class DouYinMiniGameSettingStorage : ISettingStorageBackend
    {
        [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void Register()
        {
            SettingStorageBackendFactory.RegisterMiniGameBackend(() => new DouYinMiniGameSettingStorage());
        }

        public bool Save()
        {
            return true;
        }

        public bool HasKey(string key)
        {
            return TTSDK.TTStorage.HasKeySync(key);
        }

        public bool DeleteKey(string key)
        {
            if (!TTSDK.TTStorage.HasKeySync(key))
            {
                return false;
            }

            TTSDK.TTStorage.DeleteKeySync(key);
            return true;
        }

        public void DeleteAll()
        {
            TTSDK.TTStorage.DeleteAllSync();
        }

        public int GetInt(string key, int defaultValue)
        {
            return TTSDK.TTStorage.GetIntSync(key, defaultValue);
        }

        public void SetInt(string key, int value)
        {
            TTSDK.TTStorage.SetIntSync(key, value);
        }

        public float GetFloat(string key, float defaultValue)
        {
            return TTSDK.TTStorage.GetFloatSync(key, defaultValue);
        }

        public void SetFloat(string key, float value)
        {
            TTSDK.TTStorage.SetFloatSync(key, value);
        }

        public string GetString(string key, string defaultValue)
        {
            return TTSDK.TTStorage.GetStringSync(key, defaultValue);
        }

        public void SetString(string key, string value)
        {
            TTSDK.TTStorage.SetStringSync(key, value);
        }
    }
}
