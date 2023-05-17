using ArPaint.UI.Systems.Stack;
using ArPaint.UI.Views.Draw;
using Zenject;

namespace ArPaint.Infrastructure.GameStates
{
    public class DrawState : IEnterState
    {
        private readonly IViewStack _viewStack;

        public DrawState(IViewStack viewStack)
        {
            _viewStack = viewStack;
        }

        public void OnEnter()
        {
            _viewStack.PushView<DrawView>();
        }

        public class Factory : PlaceholderFactory<DrawState>
        {
        }
    }
}