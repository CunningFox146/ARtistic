using System;
using System.Threading;
using ArPaint.UI.Views.SignIn;
using Cysharp.Threading.Tasks;
using Services.Auth;
using Services.Toast;
using UnityMvvmToolkit.Core;
using UnityMvvmToolkit.Core.Attributes;
using UnityMvvmToolkit.Core.Interfaces;
using UnityMvvmToolkit.UniTask;
using UnityMvvmToolkit.UniTask.Interfaces;

namespace ArPaint.UI.ViewModels.Register
{
    public class RegisterViewModel : ViewModel
    {
        private readonly IAuthSystem _auth;
        private readonly IToast _toast;

        [Observable(nameof(Email))] private readonly IProperty<string> _email;
        [Observable(nameof(Password))] private readonly IProperty<string> _password;
        [Observable(nameof(UserName))] private readonly IProperty<string> _userName;

        public IAsyncCommand RegisterCommand { get; }
        public ICommand OpenSignInViewCommand { get; }

        public string UserName
        {
            get => _userName.Value;
            set => _userName.Value = value;
        }

        public string Email
        {
            get => _email.Value;
            set => _email.Value = value;
        }

        public string Password
        {
            get => _password.Value;
            set => _password.Value = value;
        }

        public RegisterViewModel(IAuthSystem auth, IToast toast)
        {
            _auth = auth;
            _toast = toast;
            _userName = new Property<string>();
            _email = new Property<string>();
            _password = new Property<string>();

            OpenSignInViewCommand = new Command(OpenSignInView);
            RegisterCommand = new AsyncCommand(Register) { DisableOnExecution = true };
        }

        private void OpenSignInView()
        {
            ViewStack.PopView();
            ViewStack.PushView<SignInView>();
        }

        private async UniTask Register(CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(UserName))
            {
                _toast.ShowMessage("Please enter Email, User name & password");
                return;
            }
            
            try
            {
                await _auth.Register(Email, UserName, Password);
            }
            catch (Exception exception)
            {
                _toast.ShowMessage(exception.Message);
            }
        }
    }
}