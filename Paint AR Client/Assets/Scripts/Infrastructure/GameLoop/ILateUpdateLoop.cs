namespace ArPaint.Infrastructure.GameLoop
{
    public interface ILateUpdateLoop
    {
        void RegisterLateUpdate(ILateUpdateable updateable);
        void UnregisterLateUpdate(ILateUpdateable updateable);
    }
}