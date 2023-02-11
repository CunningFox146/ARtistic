using ArPaint.Infrastructure.GameLoop;
using ArPaint.Input;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Zenject;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace ArPaint.Infrastructure.GameStates
{
    public class DrawState : IEnterState, IExitState, IUpdateable
    {
        private readonly IUpdateLoop _loop;
        private bool _isUpdating;

        public DrawState(IUpdateLoop loop)
        {
            _loop = loop;
        }

        public void OnExit()
        {
            _loop.UnregisterUpdate(this);
        }

        public void OnEnter()
        {
            _loop.RegisterUpdate(this);
        }

        public void OnUpdate()
        {
            foreach (var touch in Touch.activeTouches)
            {
                
            }
        }

        private void OnFingerDone(Finger obj)
        {
            
        }

        public class Factory : PlaceholderFactory<DrawState>
        {
        }
    }
}