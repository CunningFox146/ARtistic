using System.Linq;
using ArPaint.UI.Systems.LoadingDisplay;
using UnityEngine.XR.ARFoundation;
using Zenject;

namespace ArPaint.Infrastructure.GameStates
{
    public class ArInitState : IEnterState, IExitState
    {
        private readonly DrawState.Factory _drawStateFactory;
        private readonly IGameStateMachine _gameState;
        private readonly ARPlaneManager _planeManager;
        private readonly ILoadingDisplaySystem _loadingDisplaySystem;

        [Inject]
        public ArInitState(ARPlaneManager planeManager, IGameStateMachine gameState, ILoadingDisplaySystem loadingDisplaySystem)
        {
            _loadingDisplaySystem = loadingDisplaySystem;
            _planeManager = planeManager;
            _gameState = gameState;
        }

        public void OnEnter()
        {
            _loadingDisplaySystem.HideLoadingView();
#if UNITY_EDITOR
            _gameState.EnterState<DrawState>();
#else
            _planeManager.planesChanged += OnPlanesChanged;
#endif
        }

        public void OnExit()
        {
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