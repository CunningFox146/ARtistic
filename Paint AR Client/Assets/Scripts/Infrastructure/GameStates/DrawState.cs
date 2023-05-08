using ArPaint.Infrastructure.GameLoop;
using ArPaint.Services.Input;
using ArPaint.UI.Systems.Stack;
using ArPaint.UI.Views.Draw;
using ArPaint.UI.Views.DrawOptions;
using ArPaint.UI.Views.Loading;
using Zenject;

namespace ArPaint.Infrastructure.GameStates
{
    public class DrawState : IEnterState, IExitState, IUpdateable
    {
        private readonly IInputSource _inputSource;
        private readonly IViewStack _viewStack;
        private readonly IUpdateLoop _updateLoop;
        private bool _isUpdating;

        public DrawState(IUpdateLoop updateLoop, IInputSource inputSource, IViewStack viewStack)
        {
            _updateLoop = updateLoop;
            _inputSource = inputSource;
            _viewStack = viewStack;
        }

        public void OnEnter()
        {
            _viewStack.PushView<DrawView>();
            _updateLoop.RegisterUpdate(this);
        }

        public void OnExit()
        {
            _updateLoop.UnregisterUpdate(this);
        }

        public void OnUpdate()
        {
            foreach (var touch in _inputSource.Touches) touch.IsOverUI();
        }

        public class Factory : PlaceholderFactory<DrawState>
        {
        }
    }
}