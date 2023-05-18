using ArPaint.UI.ViewModels.DrawingPlacer;
using Zenject;

namespace ArPaint.UI.Views.DrawingPlacer
{
    public class DrawingPlacerView : View<DrawingPlacerViewModel>
    {
        public class Factory : PlaceholderFactory<DrawingPlacerView> {}
    }
}