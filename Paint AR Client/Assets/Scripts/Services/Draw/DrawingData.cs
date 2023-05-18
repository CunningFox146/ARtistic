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
        public string Name { get; set; }
        public string Description { get; set; }
        public List<SerializableDrawCommand> DrawCommands { get; set; }
    }
}