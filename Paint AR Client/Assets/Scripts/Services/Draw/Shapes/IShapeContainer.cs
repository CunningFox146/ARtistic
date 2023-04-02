using UnityEngine;

namespace ArPaint.Services.Draw.Shapes
{
    public interface IShapeContainer
    {
        void InitTransform(Vector3 position, Quaternion rotation);
        void AppendPosition(Vector3 position);
        void SetPosition(int index, Vector3 position);
        void SetRotation(Quaternion rotation);
        void Clear();
        void Destroy();
        Vector3 TransformPoint(Vector3 worldPoint);
        bool IsLooping { get; set; }
    }
}