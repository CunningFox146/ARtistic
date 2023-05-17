using System;
using UnityEngine;

namespace Services.Toast
{
    public class ToastEditor : IToast
    {
        public void ShowError(Exception exception)
        {
            ShowMessage(exception.Message);
        }

        public void ShowMessage(string message)
            => Debug.Log($"Toast: {message}");
    }
}