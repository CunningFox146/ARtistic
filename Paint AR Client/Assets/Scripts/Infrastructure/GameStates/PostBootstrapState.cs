using ArPaint.Infrastructure.SceneManagement;
using ArPaint.UI.Systems.LoadingDisplay;
using Firebase;
using Firebase.Analytics;
using Services.Auth;
using Zenject;

namespace ArPaint.Infrastructure.GameStates
{
    public class PostBootstrapState : IEnterState, IExitState
    {
        private readonly ILoadingDisplaySystem _loadingDisplaySystem;
        private readonly IAuthSystem _auth;
        private readonly ISceneLoader _sceneLoader;

        public PostBootstrapState(ILoadingDisplaySystem loadingDisplaySystem, IAuthSystem auth, ISceneLoader sceneLoader)
        {
            _loadingDisplaySystem = loadingDisplaySystem;
            _auth = auth;
            _sceneLoader = sceneLoader;
        }
        
        public async void OnEnter()
        {
            _loadingDisplaySystem.ShowLoadingView();
            await FirebaseApp.CheckAndFixDependenciesAsync();
            FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
            await _auth.ReloadUser();

            var targetScene = _auth.IsSignedIn ? SceneIndex.Draw : SceneIndex.Auth;
            UnityEngine.Debug.Log(targetScene);
            // await _sceneLoader.LoadScene(targetScene);
        }


        public void OnExit()
        {
            _loadingDisplaySystem.ShowLoadingView();
        }

        public class Factory : PlaceholderFactory<PostBootstrapState>
        {
        }
    }
}