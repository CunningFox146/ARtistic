using System.Collections.Generic;
using System.Collections.ObjectModel;
using ArPaint.Services.Draw;
using ArPaint.Services.Draw.Shapes;
using Services.StaticData;
using UnityMvvmToolkit.Core;
using UnityMvvmToolkit.Core.Attributes;
using UnityMvvmToolkit.Core.Interfaces;

namespace ArPaint.UI.ViewModels.Draw
{
    public class DrawViewModel : ViewModel
    {
        private readonly DrawService _drawService;
        
        [Observable(nameof(Shapes))]
        private readonly IReadOnlyProperty<ObservableCollection<ShapeViewModel>> _shapes;
        
        [Observable(nameof(IsShapeSelectVisible))]
        private readonly IProperty<bool> _isShapeSelectVisible;

        public ICommand ToggleShapeSelectCommand { get; }
        public ObservableCollection<ShapeViewModel> Shapes => _shapes.Value;

        public bool IsShapeSelectVisible
        {
            get => _isShapeSelectVisible.Value;
            set => _isShapeSelectVisible.Value = value;
        }

        public DrawViewModel(IStaticDataService staticData, DrawService drawService)
        {
            _drawService = drawService;

            ToggleShapeSelectCommand = new Command(ToggleShapeSelect);
            _isShapeSelectVisible = new Property<bool>(false);
            _shapes = new ReadOnlyProperty<ObservableCollection<ShapeViewModel>>(new());
            
            InitShapes(staticData.Shapes.ShapesList);
        }

        private void ToggleShapeSelect()
        {
            IsShapeSelectVisible = !IsShapeSelectVisible;
        }

        private void InitShapes(List<Shape> shapes)
        {
            foreach (var shape in shapes)
                Shapes.Add(new ShapeViewModel(shape, OnShapeSelected));
        }

        private void OnShapeSelected(Shape shape)
        {
            foreach (var shapeViewModel in Shapes)
            {
                shapeViewModel.IsSelected = shapeViewModel.Shape == shape;
            }

            _drawService.Shape = shape;
            IsShapeSelectVisible = false;
        }
    }
}