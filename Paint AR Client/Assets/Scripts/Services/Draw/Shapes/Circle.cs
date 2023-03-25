using System.Collections.Generic;
using UnityEngine;

namespace ArPaint.Services.Draw.Shapes
{
    public class Circle : IShape
    {
        private readonly Dictionary<IShapeContainer, Vector3> _startPositions = new();
        
        public void OnDrawStart(IShapeContainer container, Vector3 position)
        {
            container.IsLooping = true;
        }

        public void OnDrawMove(IShapeContainer container, Vector3 position)
        {
            if (!_startPositions.ContainsKey(container))
            {
                _startPositions.Add(container, position);
                return;
            }
            container.Clear();
            DrawCircle(container, position);
        }

        public void OnDrawEnd(IShapeContainer container, Vector3 position)
        {
        }

        private void DrawCircle(IShapeContainer container, Vector3 position)
        {
            var radius = Vector2.Distance(_startPositions[container], position);
            for (var i = 0f; i < 360f; i++)
            {
                var angle = i * Mathf.Deg2Rad;
                var pointPos = _startPositions[container] + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
                container.AppendPosition(pointPos);
            }
        }
    }
}