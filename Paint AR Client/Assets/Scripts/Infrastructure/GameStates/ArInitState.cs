using UnityEngine.XR.ARFoundation;
using Zenject;

namespace ArPaint.Infrastructure.GameStates
{
    public class ArInitState : IState
    {
        private readonly ARPlaneManager _planeManager;
        private readonly IGameStateMachine _gameState;
        private readonly DrawState.Factory _drawStateFactory;

        [Inject]
        public ArInitState(ARPlaneManager planeManager, IGameStateMachine gameState,  DrawState.Factory drawStateFactory)
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