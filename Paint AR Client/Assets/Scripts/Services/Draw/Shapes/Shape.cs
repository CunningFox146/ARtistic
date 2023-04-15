using System;
using UnityEngine;
using UnityMvvmToolkit.Common.Interfaces;

namespace ArPaint.Services.Draw.Shapes
{
    public abstract class Shape : ScriptableObject, IShape, IShapeStart, ICollectionItemData
    {
        [field:SerializeField] public string Name { get; private set; }
        [field:SerializeField] public bool IsLooping { get; private set; }
        
        public Guid Id { get; } = Guid.NewGuid();
        
        public virtual void OnDrawStart(IShapeContainer container, Vector3 position)
        {
            container.IsLooping = IsLooping;
        }

        public abstract void OnDrawMove(IShapeContainer container, Vector3 position);
    }
}