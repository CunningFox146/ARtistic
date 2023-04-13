using UnityEngine;

namespace ArPaint.Services.Draw.Shapes
{
    [CreateAssetMenu(menuName = "Shapes/Oval")]
    public class Oval : Shape
    {
        public override void OnDrawMove(IShapeContainer container, Vector3 position)
        {
            container.Clear();
            DrawCircle(container, position);
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