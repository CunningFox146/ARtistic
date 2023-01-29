namespace ArPaint.Infrastructure.GameStates
{
    public interface IState
    {
        void OnEnter();
        void OnExit();
    }
}