using System.Collections.Generic;
using ArPaint.Services.Draw.Shapes;
using UnityEngine;

namespace Services.StaticData
{
    [CreateAssetMenu(menuName = "Shapes/Shapes Container")]
    public class Shapes : ScriptableObject, IShapes
    {
        [field: SerializeField] public List<Shape> ShapesList { get; private set; }
    }
}