using UnityEngine;

namespace ArPaint.Services.Draw.Shapes
{
    [CreateAssetMenu(menuName = "Shapes/Rectangle")]
    public class Rectangle : Shape
    {
        public override void OnDrawMove(IShapeContainer container, Vector3 position)
        {
            container.Clear();
            DrawRectangle(container, position);
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