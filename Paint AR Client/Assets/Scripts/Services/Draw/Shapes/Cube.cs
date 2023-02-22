using System.Collections.Generic;
using UnityEngine;

namespace ArPaint.Services.Draw.Shapes
{
    public class Cube : IShape
    {
        private readonly Dictionary<IShapeContainer, Vector3> _startPositions = new();
        
        public void OnDrawStart(IShapeContainer container, Vector3 position)
        {
            _startPositions.Add(container, position);
            container.IsLooping = true;
        }

        public void OnDrawMove(IShapeContainer container, Vector3 position)
        {
            if (!_startPositions.TryGetValue(container, out var startPosition)) return;
            container.Clear();
            DrawRectangle(container, startPosition, position);
        }

        private void DrawRectangle(IShapeContainer container, Vector3 startPosition, Vector3 endPosition)
        {
            var radius = Vector3.Distance(startPosition, endPosition);
            var offset = Mathf.PI / 4f;
            for (var i = 0f; i < 4f; i++)
            {
                var angle = (i / 4f) * Mathf.PI * 2f + offset;
                var pointPos = startPosition + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
                container.AppendPosition(pointPos);
            }
        }

        public void OnDrawEnd(IShapeContainer container, Vector3 position)
        {
            _startPositions.Remove(container);
        }
    }
}