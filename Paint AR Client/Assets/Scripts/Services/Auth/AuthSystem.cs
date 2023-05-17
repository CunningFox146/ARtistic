using System;
using Cysharp.Threading.Tasks;
using Firebase;
using Firebase.Analytics;
using Firebase.Auth;

namespace Services.Auth
{
    public class AuthSystem : IAuthSystem
    {
        public event Action<bool> AuthStateChange;

        private readonly FirebaseAuth _auth;

        public bool IsSignedIn => _auth.CurrentUser != null && _auth.CurrentUser.IsValid();

        public AuthSystem(FirebaseAuth auth)
        {
            _auth = auth;
            _auth.StateChanged += OnAuthStateChanged;
        }

        public async UniTask Init()
        {
            await FirebaseApp.CheckAndFixDependenciesAsync();
            FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);

            await ReloadUser();
        }

        public async UniTask SignIn(string email, string password)
        {
            var credential = EmailAuthProvider.GetCredential(email, password);
            await _auth.SignInWithCredentialAsync(credential);

            FirebaseAnalytics.LogEvent(
                FirebaseAnalytics.EventLogin, new Parameter(
                    FirebaseAnalytics.ParameterMethod, "mail"));
        }

        public async UniTask SingInWithGoogle()
        {
            FirebaseAnalytics.LogEvent(
                FirebaseAnalytics.EventSignUp, new Parameter(
                    FirebaseAnalytics.ParameterMethod, "google"));
        }

        public async UniTask Register(string email, string username, string password)
        {
            var authResult = await _auth.CreateUserWithEmailAndPasswordAsync(email, password);
            var profile = new UserProfile
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


            FirebaseAnalytics.LogEvent(
                FirebaseAnalytics.EventSignUp, new Parameter(
                    FirebaseAnalytics.ParameterMethod, "mail"));
        }

        public async UniTask ReloadUser()
        {
            if (_auth.CurrentUser != null) await _auth.CurrentUser.ReloadAsync();
        }

        public async void SignOut()
        {
            _auth.SignOut();
        }

        private void OnAuthStateChanged(object sender, EventArgs e)
        {
            AuthStateChange?.Invoke(IsSignedIn);
        }
    }
}