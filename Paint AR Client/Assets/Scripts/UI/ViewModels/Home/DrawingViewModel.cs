using System;
using ArPaint.Services.Draw;
using UnityMvvmToolkit.Common.Interfaces;
using UnityMvvmToolkit.Core;
using UnityMvvmToolkit.Core.Attributes;
using UnityMvvmToolkit.Core.Interfaces;

namespace ArPaint.UI.ViewModels.Home
{
    public class DrawingViewModel : ICollectionItem
    {
        public int Id { get; }

        [Observable(nameof(DrawingName))]
        private readonly IReadOnlyProperty<string> _drawingName;

        [Observable(nameof(DrawingDescription))]
        private readonly IReadOnlyProperty<string> _drawingDescription;

        private readonly DrawingData _drawing;
        private readonly Action<DrawingData> _selectDrawing;
        
        public ICommand SelectDrawingCommand { get; }

        public string DrawingName => _drawingName.Value;
        public string DrawingDescription => _drawingDescription.Value;
        
        public DrawingViewModel(DrawingData drawing, Action<DrawingData> selectDrawing)
        {
            _drawing = drawing;
            _selectDrawing = selectDrawing;

            _drawingName = new ReadOnlyProperty<string>(_drawing.Name);
            _drawingDescription = new ReadOnlyProperty<string>(_drawing.Description);
            
            Id = new Guid().GetHashCode();
            SelectDrawingCommand = new Command(SelectDrawing);
        }

        private void SelectDrawing()
        {
            _selectDrawing?.Invoke(_drawing);
        }
    }
}