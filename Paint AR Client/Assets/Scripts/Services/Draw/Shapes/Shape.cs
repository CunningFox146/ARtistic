using System;
using UnityEngine;
using UnityEngine.UIElements;
using UnityMvvmToolkit.Common.Interfaces;

namespace ArPaint.Services.Draw.Shapes
{
    public abstract class Shape : ScriptableObject, IShape, IShapeStart
    {
        [field:SerializeField] public string Name { get; private set; }
        [field:SerializeField] public VectorImage Icon { get; private set; }
        [field:SerializeField] public bool IsLooping { get; private set; }
        
        public virtual void OnDrawStart(IShapeContainer container, Vector3 position)
        {
            container.IsLooping = IsLooping;
        }

        public abstract void OnDrawMove(IShapeContainer container, Vector3 position);
    }
}