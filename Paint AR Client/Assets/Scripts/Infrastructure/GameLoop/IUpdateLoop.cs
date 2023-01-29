namespace ArPaint.Infrastructure.GameLoop
{
    public interface IUpdateLoop
    {
        void RegisterUpdate(IUpdateable updateable);
        void UnregisterUpdate(IUpdateable updateable);   
    }
}