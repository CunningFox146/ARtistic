namespace ArPaint.Infrastructure
{
    public interface IGameState
    {
        void EnterState<TState>() where TState : IState;
    }
}