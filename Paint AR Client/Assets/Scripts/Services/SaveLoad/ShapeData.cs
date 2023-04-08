using System;
using UnityEngine;

namespace ArPaint.Services.SaveLoad
{
    [Serializable]
    public struct ShapeData
    {
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }
        public Vector3[] LinePositions { get; set; } 
        public bool IsLooping { get; set; } 
    }
}