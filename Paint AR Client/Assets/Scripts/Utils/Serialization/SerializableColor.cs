using System;
using UnityEngine;

namespace ArPaint.Utils.Serialization
{
    [Serializable]
    public struct SerializableColor
    {
        public float R { get; set; }
        public float G { get; set; }
        public float B { get; set; }
        public float A { get; set; }

        public static SerializableColor FromColor(Color color)
            => new()
            {
                R = color.r,
                G = color.g,
                B = color.b,
                A = color.a,
            };

        public Color ToColor() => new(R, G, B, A);
        
        public static implicit operator SerializableColor(Color color) => FromColor(color);
        public static implicit operator Color(SerializableColor color) => color.ToColor();
    }
}