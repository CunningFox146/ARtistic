using System;
using ArPaint.Services.Draw;
using ArPaint.UI.Services.LoadingDisplay;
using ArPaint.UI.Services.Stack;
using ArPaint.UI.Views.Home;
using Services.PersistentData;
using Services.Toast;
using Zenject;

namespace ArPaint.Infrastructure.GameStates
{
    public class MainMenuState : IEnterState, IExitState
    {
        private readonly IDrawingsProvider _drawingsProvider;
        private readonly ILoadingDisplay _loadingDisplay;
        private readonly IPersistentData _persistentData;
        private readonly IToast _toast;
        private readonly IViewStack _viewStack;

        public MainMenuState(ILoadingDisplay loadingDisplay, IViewStack viewStack,
            IDrawingsProvider drawingsProvider, IToast toast, IPersistentData persistentData)
        {
            _loadingDisplay = loadingDisplay;
            _viewStack = viewStack;
            _drawingsProvider = drawingsProvider;
            _toast = toast;
            _persistentData = persistentData;
        }

        public async void OnEnter()
        {
            try
            {
                await _drawingsProvider.UpdateOwnedItems();
            }
            catch (Exception exception)
            {
                _toast.ShowError(exception);
            }

            _loadingDisplay.HideLoadingView();
            _viewStack.PushView<HomeView>();
        }

        public void OnExit()
        {
            _persistentData.Save();
        }

        public class Factory : PlaceholderFactory<MainMenuState>
        {
        }
    }
}