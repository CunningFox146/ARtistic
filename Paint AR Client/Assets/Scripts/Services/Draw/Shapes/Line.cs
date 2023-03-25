using System.Collections.Generic;
using UnityEngine;

namespace ArPaint.Services.Draw.Shapes
{
    public class Line : IShape
    {
        private readonly Dictionary<IShapeContainer, Vector3> _startPositions = new();
        public void OnDrawStart(IShapeContainer container, Vector3 position)
        {
            container.IsLooping = false;
            container.SetPosition(0, position);
        }

        public void OnDrawMove(IShapeContainer container, Vector3 position)
        {
            container.AppendPosition(position);
        }

        public void OnDrawEnd(IShapeContainer container, Vector3 position)
        {
            var a = container as ShapeContainer;
            var line = a.GetComponentInChildren<LineRenderer>();
            for (int i = 0; i < line.positionCount; i++)
            {
                UnityEngine.Debug.Log($"!!!{i}: {line.GetPosition(i)}");
            }
        }
    }
}