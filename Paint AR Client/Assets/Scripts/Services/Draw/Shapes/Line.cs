using UnityEngine;

namespace ArPaint.Services.Draw.Shapes
{
    [CreateAssetMenu(menuName = "Shapes/Line")]
    public class Line : Shape
    {
        public override void OnDrawMove(IShapeContainer container, Vector3 position)
        {
            container.AppendPosition(position);
        }
    }
}