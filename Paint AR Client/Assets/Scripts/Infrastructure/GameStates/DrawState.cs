using ArPaint.Infrastructure.GameLoop;
using Zenject;

namespace ArPaint.Infrastructure.GameStates
{
    public class DrawState : IState, IUpdateable
    {
        private readonly IUpdateLoop _loop;
        private bool _isUpdating;

        public DrawState(IUpdateLoop loop)
        {
            _loop = loop;
        }

        public void OnExit()
        {
            _loop.RegisterUpdate(this);
        }

        public void OnEnter()
        {
            _loop.RegisterUpdate(this);
        }

        public void OnUpdate()
        {
        }

        public class Factory : PlaceholderFactory<DrawState>
        {
        }
    }
}