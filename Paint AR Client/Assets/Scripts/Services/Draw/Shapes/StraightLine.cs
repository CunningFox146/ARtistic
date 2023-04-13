using UnityEngine;

namespace ArPaint.Services.Draw.Shapes
{
    [CreateAssetMenu(menuName = "Shapes/Straight Line")]
    public class StraightLine : Shape
    {
        public override void OnDrawStart(IShapeContainer container, Vector3 position)
        {
            base.OnDrawStart(container, position);
            container.SetPosition(0, position);
        }

        public override void OnDrawMove(IShapeContainer container, Vector3 position)
        {
            container.SetPosition(1, position);
        }
    }
}