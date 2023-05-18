using System;
using ArPaint.Utils.Serialization;
using Firebase.Firestore;
using UnityEngine;

namespace ArPaint.Services.SaveLoad
{
    public struct ShapeData
    {
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }
        public Vector3[] LinePositions { get; set; } 
        public bool IsLooping { get; set; } 
    }

    [FirestoreData]
    [Serializable]
    public class SerializableShapeData
    {
        [FirestoreProperty]
        public SerializableVector Position { get; set; }
        
        [FirestoreProperty]
        public SerializableQuaternion Rotation { get; set; }
        
        [FirestoreProperty]
        public SerializableVector[] LinePositions { get; set; } 
        
        [FirestoreProperty]
        public bool IsLooping { get; set; }

        public static SerializableShapeData FromShapeData(ShapeData data)
        {
            var serializableShapeData = new SerializableShapeData()
            {
                Position = data.Position,
                Rotation = data.Rotation,
                IsLooping = data.IsLooping,
                LinePositions = new SerializableVector[data.LinePositions.Length]
            };

            for (var i = 0; i < data.LinePositions.Length; i++)
            {
                serializableShapeData.LinePositions[i] = data.LinePositions[i];
            }

            return serializableShapeData;
        }

        public ShapeData ToShapeData()
        {
            var shapeData = new ShapeData()
            {
                Position = Position,
                Rotation = Rotation,
                IsLooping = IsLooping,
                LinePositions = new Vector3[LinePositions.Length],
            };

            for (var i = 0; i < LinePositions.Length; i++)
            {
                shapeData.LinePositions[i] = LinePositions[i];
            }
            
            return shapeData;
        }
        
        public static implicit operator SerializableShapeData(ShapeData data) => FromShapeData(data);
        public static implicit operator ShapeData(SerializableShapeData data) => data.ToShapeData();
    }
}