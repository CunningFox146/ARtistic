using System;
using System.Collections.Generic;
using ArPaint.Services.Commands;
using Firebase.Firestore;

namespace ArPaint.Services.Draw
{
    [FirestoreData]
    [Serializable]
    public class DrawingData
    {
        [FirestoreProperty]
        public int Id { get; set; }
        
        [FirestoreProperty]
        public string Name { get; set; }
        
        [FirestoreProperty]
        public string Description { get; set; }
        
        
        [FirestoreProperty]
        public List<SerializableDrawCommand> DrawCommands { get; set; }
    }
}