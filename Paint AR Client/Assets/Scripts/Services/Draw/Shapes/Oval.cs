using System.Collections.Generic;
using UnityEngine;

namespace ArPaint.Services.Draw.Shapes
{
    public class Oval : IShape
    {
        public void OnDrawStart(IShapeContainer container, Vector3 position)
        {
            container.IsLooping = true;
        }

        public void OnDrawMove(IShapeContainer container, Vector3 position)
        {
            container.Clear();
            DrawCircle(container, position);
        }

        public void OnDrawEnd(IShapeContainer container, Vector3 position)
        {
        }

        private void DrawCircle(IShapeContainer container, Vector3 position)
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