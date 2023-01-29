namespace ArPaint.Infrastructure.GameLoop
{
    public interface IFixedUpdateLoop
    {
        void RegisterFixedUpdate(IFixedUpdateable updateable);
        void UnregisterFixedUpdate(IFixedUpdateable updateable);
    }
}