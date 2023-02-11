using ArPaint.Input;
using UnityEngine.InputSystem;
using Zenject;

namespace ArPaint.Infrastructure.GameStates
{
    public class DrawState : IEnterState, IExitState
    {
        private readonly DrawActions _drawActions;
        private bool _isUpdating;

        public DrawState(DrawActions drawActions)
        {
            _drawActions = drawActions;
        }

        public void OnEnter()
        {
            _drawActions.Touch.Enable();
            _drawActions.Touch.Click.performed += ClickPerformed;
            _drawActions.Touch.Click.canceled += ClickCancelled;
        }

        public void OnExit()
        {
            _drawActions.Touch.Disable();
            _drawActions.Touch.Click.performed -= ClickPerformed;
            _drawActions.Touch.Click.canceled -= ClickCancelled;
        }

        private void ClickPerformed(InputAction.CallbackContext _)
        {
        }

        private void ClickCancelled(InputAction.CallbackContext _)
        {
        }

        public class Factory : PlaceholderFactory<DrawState>
        {
        }
    }
}