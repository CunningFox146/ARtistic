using System.Collections.Generic;
using System.Collections.ObjectModel;
using ArPaint.Services.Draw;
using ArPaint.Services.Draw.Shapes;
using ArPaint.UI.ViewModels.DrawOptions;
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

        public ObservableCollection<ShapeViewModel> Shapes => _shapes.Value;

        public DrawViewModel(IStaticDataService staticData, DrawService drawService)
        {
            _drawService = drawService;

            _shapes = new ReadOnlyProperty<ObservableCollection<ShapeViewModel>>(new());
            
            InitShapes(staticData.Shapes.ShapesList);
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
        }
    }
}