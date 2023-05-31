using ArPaint.Services.Draw.Placer;
using ArPaint.UI.Services.Stack;
using ArPaint.UI.Views.DrawingPlacer;
using Zenject;

namespace ArPaint.Infrastructure.GameStates
{
    public class PlaceDrawingState : IEnterState, IExitState
    {
        private readonly IDrawingPlacer _placer;
        private readonly IViewStack _viewStack;

        public PlaceDrawingState(IDrawingPlacer placer, IViewStack viewStack)
        {
            _placer = placer;
            _viewStack = viewStack;
        }

        public void OnEnter()
        {
            _viewStack.PushView<DrawingPlacerView>();
            _placer.StartPlacing();
        }

        public void OnExit()
        {
            _viewStack.PopView();
            _placer?.StopPlacing();
        }

        public class Factory : PlaceholderFactory<PlaceDrawingState>
        {
        }
    }
}