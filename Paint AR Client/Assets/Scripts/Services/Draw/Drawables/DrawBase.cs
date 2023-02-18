using UnityEngine;
using Zenject;

namespace ArPaint.Services.Draw.Drawables
{
    public class DrawBase : MonoBehaviour, IDrawBase
    {
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private float _distance;
        private Vector3 _lastPosition;

        public void SetPosition(Vector3 position)
        {
            SetPosition(0, position);
        }

        public void AppendPosition(Vector3 position)
        {
            if (Vector3.Distance(_lastPosition, position) < _distance) return;
            SetPosition(_lineRenderer.positionCount, position);
            _lastPosition = position;
        }

        public void SetPosition(int index, Vector3 position)
        {
            _lineRenderer.positionCount = Mathf.Max(_lineRenderer.positionCount, index + 1);
            _lineRenderer.SetPosition(index, position);
        }

        public void Clear()
        {
            _lineRenderer.positionCount = 0;
        }

        public class Factory : PlaceholderFactory<DrawBase>
        {
        }
    }
}