using UnityEngine.XR.ARFoundation;
using Zenject;

namespace ArPaint.Infrastructure.GameStates
{
    public class ArInitState : IEnterState, IExitState
    {
        private readonly DrawState.Factory _drawStateFactory;
        private readonly IGameStateMachine _gameState;
        private readonly ARPlaneManager _planeManager;

        [Inject]
        public ArInitState(ARPlaneManager planeManager, IGameStateMachine gameState, DrawState.Factory drawStateFactory)
        {
            _planeManager = planeManager;
            _gameState = gameState;
            _drawStateFactory = drawStateFactory;
        }

        public void OnEnter()
        {
            _planeManager.planesChanged += OnPlanesChanged;
        }

        public void OnExit()
        {
            _planeManager.planesChanged -= OnPlanesChanged;
        }

        private void OnPlanesChanged(ARPlanesChangedEventArgs _)
        {
            _gameState.EnterState(_drawStateFactory.Create());
        }

        public class Factory : PlaceholderFactory<ArInitState>
        {
        }
    }
}