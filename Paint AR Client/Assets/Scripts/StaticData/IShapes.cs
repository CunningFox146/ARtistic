using System.Collections.Generic;
using ArPaint.Services.Draw.Shapes;

namespace Services.StaticData
{
    public interface IShapes
    {
        List<Shape> ShapesList { get; }
    }
}