using UnityEngine;

namespace ArPaint.Services.Draw.Shapes
{
    [CreateAssetMenu(menuName = "Shapes/Cube")]
    public class Cube : Shape
    {
        public override void OnDrawMove(IShapeContainer container, Vector3 position)
        {
            container.Clear();
            DrawRectangle(container, position);
        }

        private static void DrawRectangle(IShapeContainer container, Vector3 endPosition)
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