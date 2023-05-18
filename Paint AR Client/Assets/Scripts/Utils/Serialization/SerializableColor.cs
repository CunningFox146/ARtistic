using System;
using Firebase.Firestore;
using UnityEngine;

namespace ArPaint.Utils.Serialization
{
    [FirestoreData]
    [Serializable]
    public struct SerializableColor
    {
        [FirestoreProperty]
        public float R { get; set; }
        
        [FirestoreProperty]
        public float G { get; set; }
        
        [FirestoreProperty]
        public float B { get; set; }
        
        [FirestoreProperty]
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