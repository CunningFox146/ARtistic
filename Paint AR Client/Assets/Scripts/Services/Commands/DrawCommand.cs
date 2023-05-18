using System;
using ArPaint.Services.Draw.Brushes;
using ArPaint.Services.Draw.Shapes;
using ArPaint.Services.SaveLoad;
using Firebase.Firestore;
using Newtonsoft.Json;

namespace ArPaint.Services.Commands
{
    public class DrawCommand : Command
    {
        public Brush Brush { get; set; }
        public ShapeData? ShapeData { get; set; }
        
        public IShapeContainer ShapeContainer { get; set; }
        public Func<IShapeContainer> CreateContainer { get; set; }
        
        public DrawCommand()
        {
            PerformAction = () =>
            {
                if (ShapeData != null)
                {
                    ShapeContainer = CreateContainer?.Invoke();
                    ShapeContainer?.SetBrush(Brush);
                    (ShapeContainer as ILoadable<ShapeData>)?.Load(ShapeData.Value);
                }
            };
            
            UndoAction = () =>
            {
                ShapeData = (ShapeContainer as ISavable<ShapeData>)?.GetData();
                ShapeContainer.Destroy();
            };
        }

        public string Serialize() => JsonConvert.SerializeObject((SerializableDrawCommand)this);
        public static DrawCommand Deserialize(string json) => JsonConvert.DeserializeObject(json) as SerializableDrawCommand;
    }

    
    [FirestoreData]
    [Serializable]
    public class SerializableDrawCommand
    {
        [FirestoreProperty]
        public SerializableBrush Brush { get; set; }

        [FirestoreProperty]
        public SerializableShapeData ShapeData { get; set; }
        
        public static SerializableDrawCommand FromCommand(DrawCommand brush)
            => new()
            {
                Brush = brush.Brush,
                ShapeData = brush.ShapeData,
            };

        public DrawCommand ToCommand()
            => new()
            {
                Brush = Brush,
                ShapeData = ShapeData,
            };
        
        
        public static implicit operator SerializableDrawCommand(DrawCommand command)
            => FromCommand(command);

        public static implicit operator DrawCommand(SerializableDrawCommand command)
            => command.ToCommand();
    }
}