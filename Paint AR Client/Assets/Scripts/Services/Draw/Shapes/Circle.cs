using UnityEngine;

namespace ArPaint.Services.Draw.Shapes
{
    [CreateAssetMenu(menuName = "Shapes/Circle")]
    public class Circle : Shape
    {
        public override void OnDrawMove(IShapeContainer container, Vector3 position)
        {
            container.Clear();
            DrawCircle(container, position);
        }

        private static void DrawCircle(IShapeContainer container, Vector3 position)
        {
            var radius = Vector2.Distance(Vector2.zero, position);
            for (var i = 0f; i < 360f; i++)
            {
                var angle = i * Mathf.Deg2Rad;
                var pointPos = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
                container.AppendPosition(pointPos);
            }
        }
    }
}