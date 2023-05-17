using System;
using System.Security.Authentication;
using System.Threading;
using ArPaint.UI.Views.Register;
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
    public class SignInViewModel : ViewModel
    {
        private readonly IAuthSystem _auth;
        private readonly IToast _toast;

        [Observable(nameof(Email))] private readonly IProperty<string> _email;
        [Observable(nameof(Password))] private readonly IProperty<string> _password;

        public IAsyncCommand SignInCommand { get; }
        public IAsyncCommand GoogleSignInCommand { get; }
        public ICommand OpenRegisterViewCommand { get; }

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

        public SignInViewModel(IAuthSystem auth, IToast toast)
        {
            _auth = auth;
            _toast = toast;
            _email = new Property<string>();
            _password = new Property<string>();

            SignInCommand = new AsyncCommand(SignIn) { DisableOnExecution = true };
            GoogleSignInCommand = new AsyncCommand(GoogleSignIn) { DisableOnExecution = true };
            OpenRegisterViewCommand = new Command(OpenRegisterView);
        }

        private void OpenRegisterView()
        {
            ViewStack.PopView();
            ViewStack.PushView<RegisterView>();
        }

        private async UniTask GoogleSignIn(CancellationToken cancellationToken = default)
        {
            try
            {
                await _auth.SingInWithGoogle();
            }
            catch (Exception exception)
            {
                _toast.ShowError(exception);
            }
        }

        private async UniTask SignIn(CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                _toast.ShowMessage("Please enter Email & password");
                return;
            }
            
            try
            {
                await _auth.SignIn(Email, Password);
            }
            catch (Exception exception)
            {
                _toast.ShowError(exception);
            }
        }
    }
}