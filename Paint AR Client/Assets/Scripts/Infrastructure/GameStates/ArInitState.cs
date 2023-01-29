using UnityEngine;
using UnityEngine.XR.ARFoundation;
using Zenject;

namespace ArPaint.Infrastructure.GameStates
{
    public class ArInitState : IState
    {
        private readonly ARPlaneManager _planeManager;

        [Inject]
        public ArInitState(ARPlaneManager planeManager)
        {
            _planeManager = planeManager;
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
        }
        
        public class Factory : PlaceholderFactory<ArInitState> { }
    }
}