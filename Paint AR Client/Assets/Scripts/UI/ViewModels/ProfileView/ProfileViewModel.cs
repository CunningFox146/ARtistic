using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Services.Auth;
using Services.Toast;
using UnityMvvmToolkit.Core;
using UnityMvvmToolkit.Core.Attributes;
using UnityMvvmToolkit.Core.Interfaces;
using UnityMvvmToolkit.UniTask;
using UnityMvvmToolkit.UniTask.Interfaces;

namespace ArPaint.UI.ViewModels.ProfileView
{
    public class ProfileViewModel : ViewModel
    {
        private readonly IAuthSystem _auth;
        private readonly IToast _toast;

        [Observable(nameof(Username))]
        private readonly IProperty<string> _username;
        
        public IAsyncCommand ChangeUsernameCommand { get; }
        public IAsyncCommand ForgotPasswordCommand { get; }
        public IAsyncCommand DeleteUserCommand { get; }
        public ICommand SignOutCommand { get; }

        public string Username
        {
            get => _username.Value;
            set => _username.Value = value;
        }

        public ProfileViewModel(IAuthSystem auth, IToast toast)
        {
            _auth = auth;
            _toast = toast;
            _username = new Property<string>();

            ChangeUsernameCommand = new AsyncCommand(ChangeUsername) { DisableOnExecution = true };
            ForgotPasswordCommand = new AsyncCommand(ForgotPassword) { DisableOnExecution = true };
            DeleteUserCommand = new AsyncCommand(DeleteUser) { DisableOnExecution = true };
            SignOutCommand = new Command(SignOut);
            
            Username = auth.User.DisplayName;
        }

        private async UniTask DeleteUser(CancellationToken _)
        {
            try
            {
                await _auth.DeleteProfile();
            }
            catch (Exception exception)
            {
                _toast.ShowError(exception);
            }
        }

        private async UniTask ForgotPassword(CancellationToken _)
        {
            try
            {
                await _auth.SendResetPasswordEmail();
            }
            catch (Exception exception)
            {
                _toast.ShowError(exception);
            }
        }

        private async UniTask ChangeUsername(CancellationToken _)
        {
            try
            {
                await _auth.ChangeUserName(Username);
            }
            catch (Exception exception)
            {
                _toast.ShowError(exception);
            }
        }

        private void SignOut()
        {
            _auth.SignOut();
        }
    }
}