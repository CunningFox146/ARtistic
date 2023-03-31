using System.Collections.Generic;
using UnityEngine;

namespace ArPaint.Services.Draw.Shapes
{
    public class StraightLine : IShape, IShapeStart, IShapeEnd
    {
        public void OnDrawStart(IShapeContainer container, Vector3 position)
        {
            container.IsLooping = false;
            container.SetPosition(0, position);
        }

        public void OnDrawMove(IShapeContainer container, Vector3 position)
        {
            container.SetPosition(1, position);
        }

        public void OnDrawEnd(IShapeContainer container, Vector3 position)
        {
        }
    }
}