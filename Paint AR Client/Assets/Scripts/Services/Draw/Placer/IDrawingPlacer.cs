namespace ArPaint.Services.Draw.Placer
{
    public interface IDrawingPlacer
    {
        void StartPlacing();
        void StopPlacing(bool disableContainer = false);
    }
}