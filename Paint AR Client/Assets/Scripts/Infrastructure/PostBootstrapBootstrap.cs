using ArPaint.Infrastructure.GameStates;

namespace ArPaint.Infrastructure
{
    public class PostBootstrapBootstrap
    {
        public PostBootstrapBootstrap(IGameStateMachine stateMachine, PostBootstrapState.Factory postBootstrapFactory)
        {
            stateMachine.RegisterFactory(typeof(PostBootstrapState), postBootstrapFactory);
            stateMachine.EnterState<PostBootstrapState>();
        }
    }
}