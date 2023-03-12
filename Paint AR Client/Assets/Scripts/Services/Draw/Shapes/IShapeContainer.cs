using UnityEngine;

namespace ArPaint.Services.Draw.Shapes
{
    public interface IShapeContainer
    {
        bool IsLooping { get; set; }
        int Positions { get; }
        void InitTransform(Vector3 position, Quaternion rotation);
        void SetPosition(int index, Vector3 position);
        void AppendPosition(Vector3 position);
        void Clear();
        Vector3 TransformPoint(Vector3 worldPoint);
    }
}