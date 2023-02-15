using System.Linq;
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
        public ArInitState(ARPlaneManager planeManager, IGameStateMachine gameState)
        {
            _planeManager = planeManager;
            _gameState = gameState;
        }

        public void OnEnter()
        {
            _planeManager.planesChanged += OnPlanesChanged;
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