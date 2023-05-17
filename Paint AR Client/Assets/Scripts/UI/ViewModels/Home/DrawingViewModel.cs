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
        private readonly IProperty<string> _drawingName;

        [Observable(nameof(DrawingDescription))]
        private readonly IProperty<string> _drawingDescription;

        private readonly DrawingData _drawing;
        private readonly Action<DrawingData> _selectDrawing;
        
        public ICommand SelectDrawingCommand { get; }
        
        public string DrawingName
        {
            get => _drawingName.Value;
            set => _drawingName.Value = value;
        }
        
        public string DrawingDescription
        {
            get => _drawingDescription.Value;
            set => _drawingDescription.Value = value;
        }
        
        public DrawingViewModel(DrawingData drawing, Action<DrawingData> selectDrawing)
        {
            _drawing = drawing;
            _selectDrawing = selectDrawing;

            _drawingName = new Property<string>(_drawing.Name);
            _drawingDescription = new Property<string>(_drawing.Description);
            
            Id = new Guid().GetHashCode();
            SelectDrawingCommand = new Command(SelectDrawing);

            _drawing.ItemUpdate += Update;
        }

        private void Update()
        {
            DrawingName = _drawing.Name;
            DrawingDescription = _drawing.Description;
        }

        private void SelectDrawing()
        {
            _selectDrawing?.Invoke(_drawing);
        }
    }
}