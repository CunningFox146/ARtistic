using ArPaint.UI.Systems.LoadingDisplay;
using ArPaint.UI.Systems.Stack;
using ArPaint.UI.Views.Home;
using ArPaint.UI.Views.Profile;
using Zenject;

namespace ArPaint.Infrastructure.GameStates
{
    public class MainMenuState : IEnterState
    {
        private readonly ILoadingDisplaySystem _loadingDisplay;
        private readonly IViewStack _viewStack;

        public MainMenuState(ILoadingDisplaySystem loadingDisplay, IViewStack viewStack)
        {
            _loadingDisplay = loadingDisplay;
            _viewStack = viewStack;
        }
        
        public void OnEnter()
        {
            _loadingDisplay.HideLoadingView();
            _viewStack.PushView<HomeView>();
        }
        
        public class Factory : PlaceholderFactory<MainMenuState> { }
    }
}