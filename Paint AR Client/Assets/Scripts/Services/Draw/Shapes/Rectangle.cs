using UnityEngine;

namespace ArPaint.Services.Draw.Shapes
{
    public class Rectangle : IShape, IShapeStart
    {
        public void OnDrawMove(IShapeContainer container, Vector3 position)
        {
            container.Clear();
            DrawRectangle(container, position);
        }

        public void OnDrawStart(IShapeContainer container, Vector3 position)
        {
            container.IsLooping = true;
        }

        private static void DrawRectangle(IShapeContainer container, Vector3 position)
        {
            container.AppendPosition(new Vector3(-position.x, position.y));
            container.AppendPosition(new Vector3(position.x, position.y));
            container.AppendPosition(new Vector3(position.x, -position.y));
            container.AppendPosition(new Vector3(-position.x, -position.y));
        }
    }
}