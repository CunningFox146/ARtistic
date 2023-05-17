using System;

namespace Services.Toast
{
    public interface IToast
    {
        void ShowError(Exception exception);
        void ShowMessage(string message);
    }
}