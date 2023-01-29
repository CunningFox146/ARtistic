namespace ArPaint.Infrastructure.GameStates
{
    public interface IGameStateMachine
    {
        void EnterState(IState state);
    }
}