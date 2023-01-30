namespace ArPaint.Infrastructure.GameStates
{
    public class GameStateMachine : IGameStateMachine
    {
        private IEnterState _currentState;

        public void EnterState(IEnterState stateEnter)
        {
            (_currentState as IExitState)?.OnExit();
            _currentState = stateEnter;
            _currentState?.OnEnter();
        }
    }
}