using UnityEngine;

namespace Views.Settings
{
    public class NotificationBtn : MonoBehaviour
    {
#if UNITY_IOS
    [DllImport("__Internal")]
    private static extern void _OpenSettings();
#endif

        public void OpenSettings()
        {
#if UNITY_ANDROID
            OpenAndroidSettings();
#elif UNITY_IOS
            _OpenSettings();
#endif
        }

        private void OpenAndroidSettings()
        {
            try
            {
                AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
                AndroidJavaObject intent = new AndroidJavaObject("android.content.Intent", "android.settings.SETTINGS");
                currentActivity.Call("startActivity", intent);
            }
            catch (System.Exception e)
            {
                Debug.LogError("The settings could not be opened: " + e.Message);
            }
        }
    }
}