using UnityEngine;

namespace GameFrameX.Setting.Runtime
{
    [UnityEngine.Scripting.Preserve]
    public class GameFrameXSettingCroppingHelper : MonoBehaviour
    {
        [UnityEngine.Scripting.Preserve]
        private void Start()
        {
            _ = typeof(DefaultSetting);
            _ = typeof(DefaultSettingHelper);
            _ = typeof(DefaultSettingSerializer);
            _ = typeof(PlayerPrefsSettingHelper);
            _ = typeof(SettingComponent);
            _ = typeof(SettingHelperBase);
            _ = typeof(ISettingHelper);
            _ = typeof(ISettingManager);
            _ = typeof(SettingManager);
        }
    }
}
