using System;
using UnityEngine;

namespace ArPaint.Services.Draw.Brushes
{
    public struct Brush
    {
        public Color Color { get; set; }
        public bool IsDotted { get; set; }
        public float Size { get; set; }
        public int EndVertices { get; set; }
        public int CornerVertices { get; set; }
    }
}