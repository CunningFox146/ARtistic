namespace ArPaint.Infrastructure.GameStates
{
    public interface IGameStateMachine
    {
        void EnterState<TState>() where TState : IEnterState;
    }
}