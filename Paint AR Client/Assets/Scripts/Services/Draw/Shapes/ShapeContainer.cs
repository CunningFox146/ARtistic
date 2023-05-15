using System;
using System.Collections.Generic;
using System.Linq;
using ArPaint.Services.Draw.Brushes;
using ArPaint.Services.SaveLoad;
using Unity.Mathematics;
using UnityEngine;
using Zenject;

namespace ArPaint.Services.Draw.Shapes
{
    public class ShapeContainer : MonoBehaviour, IShapeContainer, ISavable<ShapeData>, ILoadable<ShapeData>
    {
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private float _distance;
        [SerializeField] private List<ShapeMaterial> _materials;
        private Vector3 _lastPosition;

        public bool IsLooping
        {
            get => _lineRenderer.loop;
            set => _lineRenderer.loop = value;
        }

        public void SetBrush(Brush brush)
        {
            _lineRenderer.startColor = brush.Color;
            _lineRenderer.endColor = brush.Color;

            _lineRenderer.widthCurve = AnimationCurve.Constant(0f, 1f, brush.Size);
            _lineRenderer.numCapVertices = brush.Smoothness;
            _lineRenderer.numCornerVertices = brush.Smoothness;

            var materialData = _materials.FirstOrDefault(mat =>
                mat.Mode == (brush.IsDotted ? BrushMode.Dotted : BrushMode.Default));
            
            if (materialData.Material != null)
                _lineRenderer.material = materialData.Material;
        }

        public void Destroy()
        {
            GameObject.Destroy(gameObject);
        }

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

        public void SetRotation(Quaternion rotation)
        {
            transform.rotation = rotation;
        }

        public void Clear()
        {
            _lineRenderer.positionCount = 0;
        }
        
        public ShapeData GetData()
        {
            var positions = new Vector3[_lineRenderer.positionCount];
            _lineRenderer.GetPositions(positions);
            return new ShapeData
            {
                Position = transform.position,
                Rotation = transform.rotation,
                IsLooping = IsLooping,
                LinePositions = positions
            };
        }

        public void Load(ShapeData data)
        {
            transform.position = data.Position;
            transform.rotation = data.Rotation;
            
            foreach (var position in data.LinePositions)
            {
                AppendPosition(position);
            }
            _lineRenderer.loop = data.IsLooping;
        }

        public class Factory : PlaceholderFactory<ShapeContainer>
        {
        }

        [Serializable]
        private struct ShapeMaterial
        {
            [field: SerializeField] public Material Material { get; private set; }
            [field: SerializeField] public BrushMode Mode { get; private set; }
        }

    }
}