using System;
using Cysharp.Threading.Tasks;
using Firebase.Auth;

namespace Services.Auth
{
    public interface IAuthSystem
    {
        event Action<bool> AuthStateChange;
        bool IsSignedIn { get; }
        FirebaseUser User { get; }
        UniTask Init();
        UniTask SignIn(string email, string password);
        UniTask SingInWithGoogle();
        UniTask Register(string email, string username, string password);
        UniTask SendResetPasswordEmail();
        UniTask SendResetPasswordEmail(string email);
        UniTask ChangeUserName(string username);
        UniTask DeleteProfile();
        void SignOut();
    }
}