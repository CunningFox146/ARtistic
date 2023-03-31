using UnityEngine;

namespace ArPaint.Services.Draw.Shapes
{
    public class Cube : IShape, IShapeStart
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

        private void DrawRectangle(IShapeContainer container, Vector3 endPosition)
        {
            var radius = Vector3.Distance(Vector3.zero, endPosition);
            const float startAngle = Mathf.PI / 4f;
            for (var i = 0f; i < 4f; i++)
            {
                var angle = i / 4f * Mathf.PI * 2f + startAngle;
                var pointPos = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
                container.AppendPosition(pointPos);
            }
        }
    }
}