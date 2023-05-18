using System;
using Firebase.Firestore;
using UnityEngine;

namespace ArPaint.Utils.Serialization
{
    [FirestoreData]
    [Serializable]
    public struct SerializableVector
    {
        [FirestoreProperty]
        public float X { get; set; }
        
        [FirestoreProperty]
        public float Y { get; set; }
        
        [FirestoreProperty]
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