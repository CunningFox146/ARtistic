namespace ArPaint.Services.SaveLoad
{
    public interface ILoadable<in T>
    {
        void Load(T data);
    }
}