namespace ArPaint.Infrastructure.GameStates
{
    public class GameStateMachine : IGameStateMachine
    {
        private IState _currentState;

        public void EnterState(IState state)
        {
            _currentState?.OnExit();
            _currentState = state;
            _currentState?.OnEnter();
        }
    }
}