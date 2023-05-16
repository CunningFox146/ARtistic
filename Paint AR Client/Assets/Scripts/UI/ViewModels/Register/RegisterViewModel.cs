using System.Threading;
using ArPaint.UI.Views.Register;
using Cysharp.Threading.Tasks;
using Services.Auth;
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

        [Observable(nameof(UserName))]
        private readonly IProperty<string> _userName;
        
        [Observable(nameof(Login))]
        private readonly IProperty<string> _login;
        
        [Observable(nameof(Password))]
        private readonly IProperty<string> _password;
        
        public IAsyncCommand RegisterCommand { get; }

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

        public RegisterViewModel(IAuthSystem auth)
        {
            _auth = auth;
            _userName = new Property<string>();
            _login = new Property<string>();
            _password = new Property<string>();

            RegisterCommand = new AsyncCommand(Register);
        }

        private async UniTask Register(CancellationToken cancellationToken = default)
        {
            await _auth.Register(Login, UserName, Password);
        }
    }
}