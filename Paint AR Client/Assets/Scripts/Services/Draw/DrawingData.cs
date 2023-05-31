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
        public string Author { get; set; }
        
        
        [FirestoreProperty]
        public string AuthorName { get; set; }
        
        
        [FirestoreProperty]
        public DateTime CreationDate { get; set; }
        
        [FirestoreProperty]
        public string Name { get; set; }
        
        [FirestoreProperty]
        public string Description { get; set; }
        
        [FirestoreProperty]
        public List<SerializableDrawCommand> DrawCommands { get; set; }
        
        public bool IsOwned { get; set; }
        public bool IsPublished { get; set; }
        public byte[] Preview { get; set; }
    }
}