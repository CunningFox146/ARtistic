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
        private readonly IDrawService _drawService;
        private readonly IDrawingsProvider _drawingsProvider;

        public DrawState(IViewStack viewStack, IDrawingContainer drawingContainer, IDrawService drawService, IDrawingsProvider drawingsProvider)
        {
            _viewStack = viewStack;
            _drawingContainer = drawingContainer;
            _drawService = drawService;
            _drawingsProvider = drawingsProvider;
        }

        public void OnEnter()
        {
            _drawService.IsActive =
                _drawingsProvider.SelectedDrawing == null || _drawingsProvider.SelectedDrawing.IsOwned;
            
            _viewStack.PushView<DrawView>();
            _drawingContainer.Container.gameObject.SetActive(true);
        }

        public class Factory : PlaceholderFactory<DrawState>
        {
        }
    }
}