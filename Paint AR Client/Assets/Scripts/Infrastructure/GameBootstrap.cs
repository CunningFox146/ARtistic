using ArPaint.Infrastructure.GameStates;

namespace ArPaint.Infrastructure
{
    public class GameBootstrap
    {
        public GameBootstrap(IGameStateMachine stateMachine)
        {
            stateMachine.EnterState<BootstrapState>();
        }
    }
}