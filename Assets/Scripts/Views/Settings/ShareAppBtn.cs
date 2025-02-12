using RateUs;
using UnityEngine;

namespace Views.Settings
{
    public class ShareAppBtn : MonoBehaviour
    {
        private string shareMessage = "Try this awesome game! 🎮 ";
        private string key = "";

        public void Share()
        {
#if UNITY_ANDROID
            ShareAndroid();
#elif UNITY_IOS
            ShareIOS();
#endif
        }

        private void ShareAndroid()
        {
            string shareSubject = "I recommend this game!";
            
            key = Consts.KeyAndroid;
            
            if(key == "") return;
            
            string text = shareMessage + PlayerPrefs.GetString(key);

            AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
            AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");

            intentObject.Call<AndroidJavaObject>("setAction", "android.intent.action.SEND");
            intentObject.Call<AndroidJavaObject>("setType", "text/plain");
            intentObject.Call<AndroidJavaObject>("putExtra", "android.intent.extra.SUBJECT", shareSubject);
            intentObject.Call<AndroidJavaObject>("putExtra", "android.intent.extra.TEXT", text);

            AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");

            currentActivity.Call("startActivity", intentObject);
        }
        
#if UNITY_IOS
        [DllImport("__Internal")]
        private static extern void _ShareText(string message, string appURL);

        private void ShareIOS()
        {
            key = Consts.KeyIOS;

            _ShareText(shareMessage, PlayerPrefs.GetString(key));
        }
#endif
    }
}