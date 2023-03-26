using System;
using UnityEngine;
using Zenject;

namespace ArPaint.Services.Draw.Shapes
{
    public class ShapeContainer : MonoBehaviour, IShapeContainer
    {
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private float _distance;
        private Vector3 _lastPosition;

        public bool IsLooping
        {
            get => _lineRenderer.loop;
            set => _lineRenderer.loop = value;
        }

        public int Positions => _lineRenderer.positionCount;

        public Vector3 TransformPoint(Vector3 worldPosition)
        {
            return transform.InverseTransformPoint(worldPosition);
        }

        public void InitTransform(Vector3 position, Quaternion rotation)
        {
            transform.position = position;
            transform.rotation = rotation;
        }

        public void SetPosition(int index, Vector3 position)
        {
            _lineRenderer.positionCount = Mathf.Max(_lineRenderer.positionCount, index + 1);
            _lineRenderer.SetPosition(index, position);
        }

        public void AppendPosition(Vector3 position)
        {
            if (Vector3.Distance(_lastPosition, position) < _distance) return;
            SetPosition(_lineRenderer.positionCount, position);
            _lastPosition = position;
        }

        public void Clear()
        {
            _lineRenderer.positionCount = 0;
        }

        private void Update()
        {
            UnityEngine.Debug.Log($"{transform.position} {transform.eulerAngles}");
        }

        public class Factory : PlaceholderFactory<ShapeContainer>
        {
        }
    }
}