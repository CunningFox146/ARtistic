using ArPaint.Infrastructure.GameStates;

namespace ArPaint.Infrastructure
{
    public class DrawSceneBootstrap
    {
        public DrawSceneBootstrap(IGameStateMachine stateMachine)
        {
            stateMachine.EnterState<DrawState>();
        }
    }
}