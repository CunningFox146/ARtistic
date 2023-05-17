using System;
using UnityEngine;

namespace ArPaint.Utils.Serialization
{
    [Serializable]
    public class SerializableQuaternion
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float W { get; set; }

        public static SerializableQuaternion FromQuaternion(Quaternion quaternion)
            => new()
            {
                X = quaternion.x,
                Y = quaternion.y,
                Z = quaternion.z,
                W = quaternion.w,
            };

        public Quaternion ToQuaternion() => new(X, Y, Z, W);
        
        
        public static implicit operator SerializableQuaternion(Quaternion quaternion) => FromQuaternion(quaternion);
        public static implicit operator Quaternion(SerializableQuaternion quaternion) => quaternion.ToQuaternion();
    }
}