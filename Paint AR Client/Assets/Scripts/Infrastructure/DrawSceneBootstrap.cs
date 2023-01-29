using ArPaint.Infrastructure.GameStates;

namespace ArPaint.Infrastructure
{
    public class DrawSceneBootstrap
    {
        public DrawSceneBootstrap(IGameStateMachine stateMachine, ArInitState.Factory arInitStateFactory)
        {
            stateMachine.EnterState(arInitStateFactory.Create());
        }
    }
}