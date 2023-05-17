using ArPaint.Infrastructure.GameStates;

namespace ArPaint.Infrastructure
{
    public class MainMenuBootstrap
    {
        public MainMenuBootstrap(IGameStateMachine stateMachine, MainMenuState.Factory mainMenuFactory)
        {
            stateMachine.RegisterFactory(typeof(MainMenuState), mainMenuFactory);
            stateMachine.EnterState<MainMenuState>();
        }
    }
}