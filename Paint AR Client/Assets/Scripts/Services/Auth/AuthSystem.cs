using Cysharp.Threading.Tasks;
using Firebase.Auth;
using Google;
using UnityEngine;

namespace Services.Auth
{
    public class AuthSystem : IAuthSystem
    {
        private readonly FirebaseAuth _auth;
        private readonly GoogleSignIn _googleSignIn;


        public bool IsSignedIn => _auth.CurrentUser != null;

#if UNITY_EDITOR
        public AuthSystem(FirebaseAuth auth)
        {
            _auth = auth;
        }
#else
        public AuthSystem(FirebaseAuth auth, GoogleSignIn googleSignIn)
        {
            _auth = auth;
            _googleSignIn = googleSignIn;
        }
#endif
        
        public async UniTask SignIn(string email, string password)
        {
            var credential = EmailAuthProvider.GetCredential(email, password);
            await _auth.SignInWithCredentialAsync(credential);
        }

        public async UniTask SingInWithGoogle()
        {
            GoogleSignIn.Configuration = new()
            {
                WebClientId = "570735574844-jmaco85sp22e8rp1gbro0hhdj3f1u3i1.apps.googleusercontent.com",
                RequestIdToken = true,
            };
            Debug.Log(GoogleSignIn.Configuration);
            var googleUser = await _googleSignIn.SignIn();
            var credential = GoogleAuthProvider.GetCredential(googleUser.IdToken, null);
            await _auth.SignInWithCredentialAsync(credential);
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

        public void SignOut()
        {
            _auth.SignOut();
        }
    }
}