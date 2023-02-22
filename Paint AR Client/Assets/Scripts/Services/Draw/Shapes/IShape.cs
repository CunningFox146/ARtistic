using UnityEngine;

namespace ArPaint.Services.Draw.Shapes
{
    public interface IShape
    {
        void OnDrawStart(IShapeContainer container, Vector3 position);
        void OnDrawMove(IShapeContainer container, Vector3 position);
        void OnDrawEnd(IShapeContainer container, Vector3 position);
    }
}