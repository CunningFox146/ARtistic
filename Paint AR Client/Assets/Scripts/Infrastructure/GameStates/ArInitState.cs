using System.Linq;
using ArPaint.UI.Systems.LoadingDisplay;
using ArPaint.UI.Systems.Stack;
using ArPaint.UI.Views.ArInit;
using ArPaint.UI.Views.Register;
using UnityEngine.XR.ARFoundation;
using Zenject;

namespace ArPaint.Infrastructure.GameStates
{
    public class ArInitState : IEnterState, IExitState
    {
        private readonly IGameStateMachine _gameState;
        private readonly ARPlaneManager _planeManager;
        private readonly ILoadingDisplaySystem _loadingDisplaySystem;
        private readonly IViewStack _viewStack;

        [Inject]
        public ArInitState(ARPlaneManager planeManager, IGameStateMachine gameState, ILoadingDisplaySystem loadingDisplaySystem, IViewStack viewStack)
        {
            _loadingDisplaySystem = loadingDisplaySystem;
            _viewStack = viewStack;
            _planeManager = planeManager;
            _gameState = gameState;
        }

        public void OnEnter()
        {
            _loadingDisplaySystem.HideLoadingView();
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
            if (eventArgs.added.Any()) _gameState.EnterState<DrawState>();
        }

        public class Factory : PlaceholderFactory<ArInitState>
        {
        }
    }
}