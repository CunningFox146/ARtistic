using ArPaint.UI.Views.Register;
using UnityMvvmToolkit.Core;
using UnityMvvmToolkit.Core.Attributes;
using UnityMvvmToolkit.Core.Interfaces;

namespace ArPaint.UI.ViewModels.Register
{
    public class RegisterViewModel : ViewModel
    {
        [Observable(nameof(Login))]
        private readonly IProperty<string> _login;
        
        [Observable(nameof(Password))]
        private readonly IProperty<string> _password;
        
        public ICommand RegisterCommand { get; }

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

        public RegisterViewModel()
        {
            _login = new Property<string>();
            _password = new Property<string>();

            RegisterCommand = new Command(Register);
        }

        private void Register()
        {
        }
    }
}