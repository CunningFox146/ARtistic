using Firebase.Auth;

namespace Services.Auth
{
    public class AuthSystem : IAuthSystem
    {
        private readonly FirebaseAuth _auth;

        public bool IsSignedIn => _auth.CurrentUser != null;

        public AuthSystem(FirebaseAuth auth)
        {
            _auth = auth;
        }
        
        public async void SignIn(string email, string password)
        {
            var credential = EmailAuthProvider.GetCredential(email, password);
            await _auth.SignInWithCredentialAsync(credential);
        }

        public async void SingInWithGoogle()
        {
            
        }

        public async void Register(string email, string username, string password)
        {
            var authResult = await _auth.CreateUserWithEmailAndPasswordAsync(email, password);
            var profile = new UserProfile()
            {
                DisplayName = username
            };

            try
            {
                await authResult.User.UpdateUserProfileAsync(profile);
            }
            catch
            {
                await authResult.User.DeleteAsync();
                throw;
            }
        }

        public async void ReloadUser()
        {
            if (_auth.CurrentUser != null)
            {
                await _auth.CurrentUser.ReloadAsync();
            }
        }

        public async void SignOut()
        {
            _auth.SignOut();
        }
    }
}