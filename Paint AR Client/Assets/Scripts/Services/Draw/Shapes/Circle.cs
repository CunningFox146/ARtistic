using System.Collections.Generic;
using UnityEngine;

namespace ArPaint.Services.Draw.Shapes
{
    public class Circle : IShape
    {
        private Dictionary<IShapeContainer, Vector3> _startPositions = new();

        public void OnDrawStart(IShapeContainer container, Vector3 position)
        {
            container.IsLooping = true;
            _startPositions.Add(container, position);
        }

        public void OnDrawMove(IShapeContainer container, Vector3 position)
        {
            if (!_startPositions.TryGetValue(container, out var startPosition)) return;
            container.Clear();
            var radius = Vector3.Distance(startPosition, position);
            for (var i = 0f; i < 360f; i++)
            {
                var angle = i * Mathf.Deg2Rad;
                var pointPos = startPosition + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
                container.AppendPosition(pointPos);
            }
        }

        public void OnDrawEnd(IShapeContainer container, Vector3 position)
        {
            throw new System.NotImplementedException();
        }
    }
}