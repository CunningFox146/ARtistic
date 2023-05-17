using ArPaint.UI.ViewModels;
using Zenject;

namespace ArPaint.UI.Views.DrawingInfo
{
    public class DrawingInfoView : View<DrawingInfoViewModel>
    {
        public class Factory : PlaceholderFactory<DrawingInfoView> { }
    }
}