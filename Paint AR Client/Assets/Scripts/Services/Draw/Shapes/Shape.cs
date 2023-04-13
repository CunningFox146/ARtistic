using UnityEngine;

namespace ArPaint.Services.Draw.Shapes
{
    public abstract class Shape : ScriptableObject, IShape, IShapeStart
    {
        [field:SerializeField] public bool IsLooping { get; private set; }

        public virtual void OnDrawStart(IShapeContainer container, Vector3 position)
        {
            container.IsLooping = IsLooping;
        }

        public abstract void OnDrawMove(IShapeContainer container, Vector3 position);
    }
}