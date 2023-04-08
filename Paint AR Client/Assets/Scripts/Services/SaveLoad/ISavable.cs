namespace ArPaint.Services.SaveLoad
{
    public interface ISavable<out T>
    {
        T GetData();
    }
}