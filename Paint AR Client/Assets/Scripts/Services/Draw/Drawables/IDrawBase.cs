using UnityEngine;

namespace ArPaint.Services.Draw.Drawables
{
    public interface IDrawBase
    {
        void SetPosition(Vector3 position);
        void SetPosition(int index, Vector3 position);
        void AppendPosition(Vector3 position);
        void Clear();
    }
}