using ArPaint.Services.Draw;
using ArPaint.UI.Systems.Stack;
using ArPaint.UI.Views.Draw;
using Zenject;

namespace ArPaint.Infrastructure.GameStates
{
    public class DrawState : IEnterState
    {
        private readonly IViewStack _viewStack;
        private readonly IDrawingContainer _drawingContainer;

        public DrawState(IViewStack viewStack, IDrawingContainer drawingContainer)
        {
            _viewStack = viewStack;
            _drawingContainer = drawingContainer;
        }

        public void OnEnter()
        {
            _viewStack.PushView<DrawView>();
            _drawingContainer.Container.gameObject.SetActive(true);
        }

        public class Factory : PlaceholderFactory<DrawState>
        {
        }
    }
}