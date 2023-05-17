using System;
using ArPaint.Utils.Serialization;
using Newtonsoft.Json;
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

    [Serializable]
    public struct SerializableBrush
    {
        public SerializableColor Color { get; set; }
        public bool IsDotted { get; set; }
        public float Size { get; set; }
        public int Smoothness { get; set; }
        
        public static SerializableBrush FromBrush(Brush brush)
            => new()
            {
                Color = brush.Color,
                IsDotted = brush.IsDotted,
                Size = brush.Size,
                Smoothness = brush.Smoothness
            };

        public Brush ToBrush()
            => new()
            {
                Color = Color,
                IsDotted = IsDotted,
                Size = Size,
                Smoothness = Smoothness
            };
        
        
        public static implicit operator SerializableBrush(Brush brush)
            => FromBrush(brush);

        public static implicit operator Brush(SerializableBrush brush)
            => brush.ToBrush();
    }
}