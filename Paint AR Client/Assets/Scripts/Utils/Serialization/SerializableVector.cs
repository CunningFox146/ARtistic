using System;
using UnityEngine;

namespace ArPaint.Utils.Serialization
{
    [Serializable]
    public struct SerializableVector
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public static SerializableVector FromVector3(Vector3 vector3)
            => new()
            {
                X = vector3.x,
                Y = vector3.y,
                Z = vector3.z,
            };

        public Vector3 ToVector3() => new(X, Y, Z);


        public static implicit operator SerializableVector(Vector3 vector3) => FromVector3(vector3);
        public static implicit operator Vector3(SerializableVector vector3) => vector3.ToVector3();
    }
}