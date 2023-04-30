using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using ArPaint.Services.Draw;
using ArPaint.Services.Draw.Shapes;
using Services.StaticData;
using UnityMvvmToolkit.Core;

namespace ArPaint.UI.ViewModels.DrawOptions
{
    public class DrawOptionsViewModel : ViewModel
    {
        private readonly DrawService _drawService;
        public ObservableCollection<ShapeViewData> Shapes { get; } = new();

        public DrawOptionsViewModel(IStaticDataService staticData, DrawService drawService)
        {
            _drawService = drawService;
            InitShapes(staticData.Shapes.ShapesList);
        }

        private void InitShapes(List<Shape> shapes)
        {
            foreach (var shape in shapes) Shapes.Add(new ShapeViewData(shape));

            Shapes.CollectionChanged += OnShapesChanged;
        }

        private void OnShapesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            _drawService.Shape = Shapes.FirstOrDefault(shape => shape.IsSelected)?.Shape;
            OnPropertyChanged(nameof(Shapes));
        }
    }
}