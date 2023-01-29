using ArPaint.Infrastructure.GameStates;

namespace ArPaint.Infrastructure
{
    public class GameBootstrap
    {
        public GameBootstrap(IGameStateMachine stateMachine, BootstrapState.Factory bootstrapFactory)
        {
            stateMachine.EnterState(bootstrapFactory.Create());
        }
    }
}