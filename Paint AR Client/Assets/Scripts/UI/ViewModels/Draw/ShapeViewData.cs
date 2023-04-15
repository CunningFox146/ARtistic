using ArPaint.Services.Draw.Shapes;

namespace ArPaint.UI.ViewModels.Draw
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