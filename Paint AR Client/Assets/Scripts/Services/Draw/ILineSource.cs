namespace ArPaint.Services.Draw
{
    public interface ILineSource
    {
        Line Get();
        void Release(Line line);
    }
}