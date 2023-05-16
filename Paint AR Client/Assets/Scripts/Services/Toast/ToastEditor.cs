using UnityEngine;

namespace Services.Toast
{
    public class ToastEditor : IToast
    {
        public void ShowMessage(string message)
            => Debug.Log($"Toast: {message}");
    }
}