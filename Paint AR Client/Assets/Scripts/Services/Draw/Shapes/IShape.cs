using UnityEngine;

namespace ArPaint.Services.Draw.Shapes
{
    public interface IShape
    {
        void OnDrawMove(IShapeContainer container, Vector3 position);
    }
    
    public interface IShapeStart
    {
        void OnDrawStart(IShapeContainer container, Vector3 position);
    }
    
    public interface IShapeEnd
    {
        void OnDrawEnd(IShapeContainer container, Vector3 position);
    }
}