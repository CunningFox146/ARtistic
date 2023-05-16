namespace Services.Auth
{
    public interface IAuthSystem
    {
        bool IsSignedIn { get; }
        void SignIn(string email, string password);
        void SingInWithGoogle();
        void Register(string email, string username, string password);
        void ReloadUser();
        void SignOut();
    }
}