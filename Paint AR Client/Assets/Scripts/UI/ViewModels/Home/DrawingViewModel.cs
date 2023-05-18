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

        [Observable(nameof(DrawingName))]
        private readonly IProperty<string> _drawingName;

        [Observable(nameof(DrawingDescription))]
        private readonly IProperty<string> _drawingDescription;

        private readonly Action<DrawingData> _selectDrawing;

        public int Id { get; }
        public DrawingData Drawing { get; }
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
            Drawing = drawing;
            Id = Drawing.Id;
            
            _selectDrawing = selectDrawing;

            _drawingName = new Property<string>(Drawing.Name);
            _drawingDescription = new Property<string>(Drawing.Description);

            SelectDrawingCommand = new Command(SelectDrawing);
            Update();
        }

        private void Update()
        {
            DrawingName = Drawing.Name;
            DrawingDescription = Drawing.Description;
        }

        private void SelectDrawing()
        {
            _selectDrawing?.Invoke(Drawing);
        }
    }
}