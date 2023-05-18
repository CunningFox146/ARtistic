using UnityEngine;

namespace ArPaint.Services.Draw
{
    public interface IDrawingContainer
    {
        Transform Container { get; }
    }
}