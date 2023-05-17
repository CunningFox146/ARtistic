using System;
using ArPaint.Infrastructure.SceneManagement;
using Cysharp.Threading.Tasks;
using Firebase;
using Firebase.Analytics;
using Firebase.Auth;
using UnityEngine;

namespace Services.Auth
{
    public class AuthSystem : IAuthSystem
    {
        public event Action<bool> AuthStateChange;

        private readonly FirebaseAuth _auth;
        private readonly ISceneLoader _sceneLoader;

        public bool IsSignedIn => _auth.CurrentUser != null && _auth.CurrentUser.IsValid();
        public FirebaseUser User => _auth.CurrentUser;

        public AuthSystem(FirebaseAuth auth, ISceneLoader sceneLoader)
        {
            _auth = auth;
            _sceneLoader = sceneLoader;
            _auth.StateChanged += OnAuthStateChanged;
        }

        public async UniTask Init()
        {
            await FirebaseApp.CheckAndFixDependenciesAsync();
            FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);

            try
            {
                await ReloadUser();
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
            }
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
                await authResult.User.SendEmailVerificationAsync();
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

        private async UniTask ReloadUser()
        {
            if (_auth.CurrentUser != null) await _auth.CurrentUser.ReloadAsync();
        }

        public async UniTask SendResetPasswordEmail()
        {
            if (IsSignedIn)
            {
                await _auth.SendPasswordResetEmailAsync(_auth.CurrentUser.Email);
                SignOut();
            }
        }

        public async UniTask ChangeUserName(string username)
        {
            if (!IsSignedIn)
                return;
            
            var profile = new UserProfile
            {
                DisplayName = username
            };

            await _auth.CurrentUser.UpdateUserProfileAsync(profile);
        }

        public async UniTask DeleteProfile()
        {
            if (IsSignedIn)
            {
                await _auth.CurrentUser.DeleteAsync();
                SignOut();
            }
        }

        public void SignOut()
        {
            _auth.SignOut();
            _sceneLoader.LoadScene(SceneIndex.Auth);
        }

        private void OnAuthStateChanged(object sender, EventArgs e)
        {
            AuthStateChange?.Invoke(IsSignedIn);
        }
    }
}