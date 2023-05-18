using ArPaint.Infrastructure.GameStates;

namespace ArPaint.Infrastructure
{
    public class DrawSceneBootstrap
    {
        public DrawSceneBootstrap(IGameStateMachine stateMachine, ArInitState.Factory arInitStateFactory,
            DrawState.Factory drawStateFactory, PlaceDrawingState.Factory placeFactory)
        {
            stateMachine.RegisterFactory(typeof(ArInitState), arInitStateFactory);
            stateMachine.RegisterFactory(typeof(PlaceDrawingState), placeFactory);
            stateMachine.RegisterFactory(typeof(DrawState), drawStateFactory);
            stateMachine.EnterState<ArInitState>();
        }
    }
}