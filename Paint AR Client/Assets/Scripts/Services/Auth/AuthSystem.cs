using Cysharp.Threading.Tasks;
using Firebase.Auth;
using UnityEngine;
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
        
        public async UniTask SignIn(string email, string password)
        {
            var credential = EmailAuthProvider.GetCredential(email, password);
            await _auth.SignInWithCredentialAsync(credential);
        }

        public async UniTask SingInWithGoogle()
        {
            
        }

        public async UniTask Register(string email, string username, string password)
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

        public async UniTask ReloadUser()
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