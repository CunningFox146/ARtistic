using UnityEngine;

namespace ArPaint.Services.Draw.Shapes
{
    public class Oval : IShape, IShapeStart
    {
        public void OnDrawMove(IShapeContainer container, Vector3 position)
        {
            container.Clear();
            DrawCircle(container, position);
        }

        public void OnDrawStart(IShapeContainer container, Vector3 position)
        {
            container.IsLooping = true;
        }

        private static void DrawCircle(IShapeContainer container, Vector3 position)
        {
            for (var i = 0f; i < 360f; i++)
            {
                var angle = i * Mathf.Deg2Rad;
                var pointPos = new Vector3(Mathf.Cos(angle) * position.x, Mathf.Sin(angle) * position.y);
                container.AppendPosition(pointPos);
            }
        }
    }
}