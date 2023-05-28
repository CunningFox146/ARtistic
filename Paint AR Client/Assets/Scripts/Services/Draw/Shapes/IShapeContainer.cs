using ArPaint.Services.Draw.Brushes;
using UnityEngine;

namespace ArPaint.Services.Draw.Shapes
{
    public interface IShapeContainer
    {
        int PositionsCount { get; }
        bool IsLooping { get; set; }
        
        void SetParent(Transform parent);
        void InitTransform(Vector3 position, Quaternion rotation);
        void AppendPosition(Vector3 position);
        void SetPosition(int index, Vector3 position);
        void SetRotation(Quaternion rotation);
        void Clear();
        Vector3 TransformPoint(Vector3 worldPoint);
        void SetBrush(Brush brush);
        void Destroy();
    }
}