using ArPaint.Services.Draw;
using UnityEngine;
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
        private readonly IProperty<Color> _shapeColor;
        
        [Observable(nameof(Size))]
        private readonly IProperty<float> _size;
        
        [Observable(nameof(Smoothness))]
        private readonly IProperty<float> _smoothness;
        
        [Observable(nameof(IsDotted))]
        private readonly IProperty<bool> _isDotted;
        
        public ICommand CloseViewCommand { get; }

        public Color ShapeColor
        {
            get => _shapeColor.Value;
            set => _shapeColor.Value = value;
        }
        
        public float Size
        {
            get => _size.Value;
            set => _size.Value = value;
        }
        
        public float Smoothness
        {
            get => _smoothness.Value;
            set => _smoothness.Value = value;
        }
        
        public bool IsDotted
        {
            get => _isDotted.Value;
            set => _isDotted.Value = value;
        }

        public DrawOptionsViewModel(IDrawService drawService)
        {
            _drawService = drawService;
            
            _shapeColor = new Property<Color>();
            _size = new Property<float>();
            _smoothness = new Property<float>();
            _isDotted = new Property<bool>();
            
            ShapeColor = drawService.Brush.Color;
            Size = drawService.Brush.Size;
            Smoothness = drawService.Brush.Smoothness;
            IsDotted = drawService.Brush.IsDotted;

            CloseViewCommand = new Command(CloseView);
        }

        private void CloseView()
        {
            _drawService.Brush = new()
            {
                Color = ShapeColor,
                Size = Size,
                Smoothness = Mathf.FloorToInt(Smoothness),
                IsDotted = IsDotted,
            };
            ViewStack.PopView();
        }

    }
}