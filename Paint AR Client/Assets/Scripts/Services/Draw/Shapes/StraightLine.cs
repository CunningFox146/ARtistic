using System.Collections.Generic;
using UnityEngine;

namespace ArPaint.Services.Draw.Shapes
{
    public class StraightLine : IShape
    {
        private readonly Dictionary<IShapeContainer, Vector3> _startPositions = new();
        
        public void OnDrawStart(IShapeContainer container, Vector3 position)
        {
            container.IsLooping = false;
            container.SetPosition(0, Vector3.zero);
        }

        public void OnDrawMove(IShapeContainer container, Vector3 position)
        {
            if (!_startPositions.ContainsKey(container))
            {
                _startPositions.Add(container, position);
                container.SetPosition(0, position);
                return;
            }
            container.SetPosition(1, position);
        }

        public void OnDrawEnd(IShapeContainer container, Vector3 position)
        {
            _startPositions.Remove(container);
        }
    }
}