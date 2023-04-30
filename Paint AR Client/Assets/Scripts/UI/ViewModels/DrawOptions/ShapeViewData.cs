using ArPaint.Services.Draw.Shapes;

namespace ArPaint.UI.ViewModels.DrawOptions
{
    public class ShapeViewData
    {
        public Shape Shape { get; }
        public bool IsSelected { get; set; }

        public ShapeViewData(Shape shape, bool isSelected = false)
        {
            Shape = shape;
            IsSelected = isSelected;
        }
    }
}