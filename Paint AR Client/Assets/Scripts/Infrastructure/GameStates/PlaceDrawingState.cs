using ArPaint.Services.Draw.Placer;
using Zenject;

namespace ArPaint.Infrastructure.GameStates
{
    public class PlaceDrawingState : IEnterState, IExitState
    {
        private readonly IDrawingPlacer _placer;

        public PlaceDrawingState(IDrawingPlacer placer)
        {
            _placer = placer;
        }

        public void OnEnter()
        {
            _placer.StartPlacing();
        }

        public void OnExit()
        {
            _placer.StopPlacing();
        }

        public class Factory : PlaceholderFactory<PlaceDrawingState>
        {
        }
    }
}