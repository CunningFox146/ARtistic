using System.Linq;
using ArPaint.Services.Draw;
using ArPaint.UI.Systems.LoadingDisplay;
using ArPaint.UI.Systems.Stack;
using ArPaint.UI.Views.ArInit;
using UnityEngine.XR.ARFoundation;
using Zenject;

namespace ArPaint.Infrastructure.GameStates
{
    public class ArInitState : IEnterState, IExitState
    {
        private readonly IDrawingsProvider _drawingsProvider;
        private readonly IGameStateMachine _gameState;
        private readonly ILoadingDisplay loadingDisplay;
        private readonly ARPlaneManager _planeManager;
        private readonly IViewStack _viewStack;

        [Inject]
        public ArInitState(ARPlaneManager planeManager, IGameStateMachine gameState,
            ILoadingDisplay loadingDisplay, IViewStack viewStack, IDrawingsProvider drawingsProvider)
        {
            this.loadingDisplay = loadingDisplay;
            _viewStack = viewStack;
            _drawingsProvider = drawingsProvider;
            _planeManager = planeManager;
            _gameState = gameState;
        }

        public void OnEnter()
        {
            loadingDisplay.HideLoadingView();

#if UNITY_EDITOR
            _gameState.EnterState<DrawState>();
#else
            _viewStack.PushView<ArInitView>();
            _planeManager.planesChanged += OnPlanesChanged;
#endif
        }

        public void OnExit()
        {
            _viewStack.PopView();
            _planeManager.planesChanged -= OnPlanesChanged;
        }

        private void OnPlanesChanged(ARPlanesChangedEventArgs eventArgs)
        {
            if (!eventArgs.added.Any())
                return;

            if (_drawingsProvider.SelectedDrawing is { DrawCommands: not null } &&
                _drawingsProvider.SelectedDrawing.DrawCommands.Any())
                _gameState.EnterState<PlaceDrawingState>();
            else
                _gameState.EnterState<DrawState>();
        }

        public class Factory : PlaceholderFactory<ArInitState>
        {
        }
    }
}