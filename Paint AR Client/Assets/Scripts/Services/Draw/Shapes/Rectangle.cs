using System.Collections.Generic;
using UnityEngine;

namespace ArPaint.Services.Draw.Shapes
{
    public class Rectangle : IShape
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
            var scale = endPosition - startPosition;
            container.AppendPosition(startPosition + new Vector3(-scale.x, scale.y));
            container.AppendPosition(startPosition + new Vector3(scale.x, scale.y));
            container.AppendPosition(startPosition + new Vector3(scale.x, -scale.y));
            container.AppendPosition(startPosition + new Vector3(-scale.x, -scale.y));
        }

        public void OnDrawEnd(IShapeContainer container, Vector3 position)
        {
            _startPositions.Remove(container);
        }
    }
}