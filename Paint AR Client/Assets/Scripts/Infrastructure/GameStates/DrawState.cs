using ArPaint.Infrastructure.GameLoop;
using ArPaint.Services.Input;
using ArPaint.UI.Views.Draw;
using UI.Systems;
using Zenject;

namespace ArPaint.Infrastructure.GameStates
{
    public class DrawState : IEnterState, IExitState, IUpdateable
    {
        private readonly IInputSource _inputSource;
        private readonly IUpdateLoop _updateLoop;
        private bool _isUpdating;

        public DrawState(IUpdateLoop updateLoop, IInputSource inputSource, IViewStack viewStack)
        {
            _updateLoop = updateLoop;
            _inputSource = inputSource;
            viewStack.PushView<DrawView>();
        }

        public void OnEnter()
        {
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