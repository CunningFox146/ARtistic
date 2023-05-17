using System;
using UnityEngine;

namespace Services.Toast
{
    public class ToastAndroid : IToast
    {
        public void ShowError(Exception exception)
        {
            var message = exception.Message;
            const string start = "One or more errors occurred. ";
            if (message.StartsWith(start))
            {
                message = message[(start.Length - 1)..];
            }

            message = message.Substring(2, message.Length - 3);
            ShowMessage(message);
        }
        
        public void ShowMessage(string message)
        {
            var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            var unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

            if (unityActivity == null)
                return;
            
            var toastClass = new AndroidJavaClass("android.widget.Toast");
            unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                var toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", unityActivity, message, 0);
                toastObject.Call("show");
            }));
        }
    }
}