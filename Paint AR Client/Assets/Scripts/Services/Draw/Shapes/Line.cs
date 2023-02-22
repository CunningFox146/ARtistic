using UnityEngine;

namespace ArPaint.Services.Draw.Shapes
{
    public class Line : IShape
    {
        public void OnDrawStart(IShapeContainer container, Vector3 position)
        {
            container.IsLooping = false;
            container.SetPosition(0, position);
        }

        public void OnDrawMove(IShapeContainer container, Vector3 position)
        {
            container.AppendPosition(position);
        }

        public void OnDrawEnd(IShapeContainer container, Vector3 position)
        {
            
        }
    }
}