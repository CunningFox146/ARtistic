using ArPaint.Infrastructure.GameStates;

namespace ArPaint.Infrastructure
{
    public class AuthSceneBootstrap
    {
        public AuthSceneBootstrap(IGameStateMachine stateMachine, AuthState.Factory authStateFactory)
        {
            stateMachine.RegisterFactory(typeof(AuthState), authStateFactory);
            stateMachine.EnterState<AuthState>();
        }
    }
}