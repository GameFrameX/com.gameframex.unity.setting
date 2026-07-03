namespace GameFrameX.Setting.Runtime
{
    /// <summary>
    /// Setting helper 内部使用的最小键值存储后端契约。
    /// </summary>
    /// <remarks>
    /// Internal key-value storage backend contract used by setting helpers.
    /// </remarks>
    internal interface ISettingStorageBackend
    {
        bool Save();

        bool HasKey(string key);

        bool DeleteKey(string key);

        void DeleteAll();

        int GetInt(string key, int defaultValue);

        void SetInt(string key, int value);

        float GetFloat(string key, float defaultValue);

        void SetFloat(string key, float value);

        string GetString(string key, string defaultValue);

        void SetString(string key, string value);
    }
}
