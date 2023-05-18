using ArPaint.Infrastructure.SceneManagement;
using ArPaint.UI.Systems.LoadingDisplay;
using ArPaint.UI.Systems.Stack;
using ArPaint.UI.Views.SignIn;
using Services.Auth;
using Services.PersistentData;
using Zenject;

namespace ArPaint.Infrastructure.GameStates
{
    public class AuthState : IEnterState, IExitState
    {
        private readonly IAuthSystem _auth;
        private readonly ILoadingDisplaySystem _loadingDisplay;
        private readonly IPersistentData _persistentData;
        private readonly ISceneLoader _sceneLoader;
        private readonly IViewStack _viewStack;

        public AuthState(IAuthSystem auth, ILoadingDisplaySystem loadingDisplay, IViewStack viewStack,
            ISceneLoader sceneLoader, IPersistentData persistentData)
        {
            _auth = auth;
            _loadingDisplay = loadingDisplay;
            _viewStack = viewStack;
            _sceneLoader = sceneLoader;
            _persistentData = persistentData;
        }

        public void OnEnter()
        {
            _persistentData.Clear();
            _loadingDisplay.HideLoadingView();
            _viewStack.PushView<SignInView>();
            _auth.AuthStateChange += OnAuthStateChange;
        }

        public void OnExit()
        {
            _auth.AuthStateChange -= OnAuthStateChange;
        }

        private async void OnAuthStateChange(bool isSignedIn)
        {
            if (isSignedIn)
            {
                _auth.AuthStateChange -= OnAuthStateChange;
                await _sceneLoader.LoadScene(SceneIndex.MainMenu);
            }
        }

        public class Factory : PlaceholderFactory<AuthState>
        {
        }
    }
}