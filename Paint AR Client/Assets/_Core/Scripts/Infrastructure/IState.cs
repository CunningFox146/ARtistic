namespace ArPaint.Infrastructure
{
    public interface IState
    {
        void OnExit();
        void OnEnter();
    }
}