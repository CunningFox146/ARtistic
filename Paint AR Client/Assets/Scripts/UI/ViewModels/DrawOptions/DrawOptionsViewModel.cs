using System.Drawing;
using ArPaint.Services.Draw;
using UnityEngine.XR.ARFoundation;
using UnityMvvmToolkit.Core;
using UnityMvvmToolkit.Core.Attributes;
using UnityMvvmToolkit.Core.Interfaces;
using Color = UnityEngine.Color;

namespace ArPaint.UI.ViewModels.DrawOptions
{
    public class DrawOptionsViewModel : ViewModel
    {
        private readonly IDrawService _drawService;

        [Observable(nameof(ShapeColor))]
        private readonly Property<Color> _shapeColor;
        
        public ICommand CloseViewCommand { get; }
        public Brush _brush;

        public Color ShapeColor
        {
            get => _shapeColor.Value;
            set => _shapeColor.Value = value;
        }

        public DrawOptionsViewModel(IDrawService drawService)
        {
            _drawService = drawService;
            
            _shapeColor = new Property<Color>();
            _shapeColor.Value = drawService.Brush.Color;

            CloseViewCommand = new Command(CloseView);
        }

        private void CloseView()
        {
            _drawService.Brush = new()
            {
                Color = _shapeColor.Value
            };
            ViewStack.PopView();
        }

    }
}