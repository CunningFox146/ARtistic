using System.Collections.Generic;
using UnityEngine;

namespace ArPaint.Services.Draw.Shapes
{
    public class Line : IShape, IShapeStart
    {
        public void OnDrawStart(IShapeContainer container, Vector3 position)
        {
            container.IsLooping = false;
        }

        public void OnDrawMove(IShapeContainer container, Vector3 position)
        {
            container.AppendPosition(position);
        }
    }
}