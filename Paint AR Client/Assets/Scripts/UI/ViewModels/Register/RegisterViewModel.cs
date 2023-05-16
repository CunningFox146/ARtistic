using System;
using System.Security.Authentication;
using System.Threading;
using Cysharp.Threading.Tasks;
using Firebase;
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

        [Observable(nameof(Login))] private readonly IProperty<string> _login;

        [Observable(nameof(Password))] private readonly IProperty<string> _password;

        private readonly IToast _toast;

        [Observable(nameof(UserName))] private readonly IProperty<string> _userName;

        public IAsyncCommand RegisterCommand { get; }
        public IAsyncCommand GoogleSignInCommand { get; }

        public string UserName
        {
            get => _userName.Value;
            set => _userName.Value = value;
        }

        public string Login
        {
            get => _login.Value;
            set => _login.Value = value;
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
            _login = new Property<string>();
            _password = new Property<string>();

            RegisterCommand = new AsyncCommand(Register) { DisableOnExecution = true };
            GoogleSignInCommand = new AsyncCommand(GoogleSignIn) { DisableOnExecution = true };
        }

        private async UniTask GoogleSignIn(CancellationToken cancellationToken = default)
        {
            try
            {
                await _auth.SingInWithGoogle();
            }
            catch (Exception exception)
            {
                _toast.ShowMessage(exception.Message);
            }
        }

        private async UniTask Register(CancellationToken cancellationToken = default)
        {
            try
            {
                await _auth.Register(Login, UserName, Password);
            }
            catch (Exception exception)
            {
                _toast.ShowMessage(exception.Message);
            }
        }
    }
}