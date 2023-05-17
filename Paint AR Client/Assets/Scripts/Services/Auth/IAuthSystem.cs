using System;
using Cysharp.Threading.Tasks;

namespace Services.Auth
{
    public interface IAuthSystem
    {
        event Action<bool> AuthStateChange;
        bool IsSignedIn { get; }
        UniTask Init();
        UniTask SignIn(string email, string password);
        UniTask SingInWithGoogle();
        UniTask Register(string email, string username, string password);
        UniTask ReloadUser();
        void SignOut();
    }
}