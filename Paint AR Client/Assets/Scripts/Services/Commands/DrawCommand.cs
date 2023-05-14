using System;
using ArPaint.Services.Draw.Brushes;
using ArPaint.Services.Draw.Shapes;
using ArPaint.Services.SaveLoad;

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
    }
}