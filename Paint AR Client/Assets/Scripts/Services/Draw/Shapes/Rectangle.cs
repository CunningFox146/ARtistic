using UnityEngine;

namespace ArPaint.Services.Draw.Shapes
{
    public class Rectangle : IShape
    {
        public void OnDrawStart(IShapeContainer container, Vector3 position)
        {
            container.IsLooping = true;
        }

        public void OnDrawMove(IShapeContainer container, Vector3 position)
        {
            container.Clear();
            DrawRectangle(container, position);
        }

        public void OnDrawEnd(IShapeContainer container, Vector3 position)
        {
        }

        private void DrawRectangle(IShapeContainer container, Vector3 position)
        {
            container.AppendPosition(new Vector3(-position.x, position.y));
            container.AppendPosition(new Vector3(position.x, position.y));
            container.AppendPosition(new Vector3(position.x, -position.y));
            container.AppendPosition(new Vector3(-position.x, -position.y));
        }
    }
}