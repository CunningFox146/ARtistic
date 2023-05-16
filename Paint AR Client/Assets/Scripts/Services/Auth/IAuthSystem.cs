using Cysharp.Threading.Tasks;

namespace Services.Auth
{
    public interface IAuthSystem
    {
        bool IsSignedIn { get; }
        UniTask SignIn(string email, string password);
        UniTask SingInWithGoogle();
        UniTask Register(string email, string username, string password);
        UniTask ReloadUser();
        void SignOut();
    }
}