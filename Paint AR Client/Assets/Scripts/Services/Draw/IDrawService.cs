using ArPaint.Services.Draw.Shapes;

namespace ArPaint.Services.Draw
{
    public interface IDrawService
    {
        IShape Shape { get; set; }
    }
}