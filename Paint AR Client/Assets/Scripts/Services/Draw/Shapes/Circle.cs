﻿using System.Collections.Generic;
using UnityEngine;

namespace ArPaint.Services.Draw.Shapes
{
    public class Circle : IShape
    {
        private readonly Dictionary<IShapeContainer, Vector3> _startPositions = new();

        public void OnDrawStart(IShapeContainer container, Vector3 position)
        {
            container.IsLooping = true;
            _startPositions.Add(container, position);
        }

        public void OnDrawMove(IShapeContainer container, Vector3 position)
        {
            if (!_startPositions.TryGetValue(container, out var startPosition)) return;
            
            container.Clear();
            DrawCircle(container, startPosition, position);
        }

        public void OnDrawEnd(IShapeContainer container, Vector3 position)
        {
            _startPositions.Remove(container);
        }

        private void DrawCircle(IShapeContainer container, Vector3 startPosition, Vector3 endPosition)
        {
            var radius = Vector3.Distance(startPosition, endPosition);
            for (var i = 0f; i < 360f; i++)
            {
                var angle = i * Mathf.Deg2Rad;
                var pointPos = startPosition + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
                container.AppendPosition(pointPos);
            }
        }
    }
}