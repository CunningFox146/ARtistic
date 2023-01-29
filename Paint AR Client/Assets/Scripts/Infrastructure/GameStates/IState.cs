namespace ArPaint.Infrastructure.GameStates
{
    public interface IState
    {
        void OnExit();
        void OnEnter();
    }
}