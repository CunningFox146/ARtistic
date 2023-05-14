using ArPaint.Services.Draw;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityMvvmToolkit.Core;
using UnityMvvmToolkit.Core.Attributes;
using UnityMvvmToolkit.Core.Interfaces;

namespace ArPaint.UI.ViewModels.DrawOptions
{
    public class DrawOptionsViewModel : ViewModel
    {
        private readonly DrawService _drawService;

        [Observable(nameof(ShapeColor))]
        private readonly Property<Color> _shapeColor;
        
        public ICommand CloseViewCommand { get; }

        public Color ShapeColor
        {
            get => _shapeColor.Value;
            set => _shapeColor.Value = value;
        }

        public DrawOptionsViewModel(DrawService drawService)
        {
            _drawService = drawService;
            
            _shapeColor = new Property<Color>();
            _shapeColor.ValueChanged += OnShapeColorChanged;

            CloseViewCommand = new Command(CloseView);
        }

        private void CloseView()
        {
            ViewStack.PopView();
        }

        private void OnShapeColorChanged(object sender, Color color)
        {
            
        }
    }
}