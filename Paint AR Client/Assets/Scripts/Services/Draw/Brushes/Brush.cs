using System;
using UnityEngine;

namespace ArPaint.Services.Draw.Brushes
{
    public struct Brush
    {
        public Color Color { get; set; }
        public bool IsDotted { get; set; }
        public float Size { get; set; }
        public int Smoothness { get; set; }
        
        public static Brush Default => new()
        {
            Color = Color.white,
            IsDotted = false,
            Size= 1f,
            Smoothness = 0,
        };
    }
}