using ArPaint.UI.ViewModels;
using ArPaint.UI.ViewModels.DrawingInfo;
using Zenject;

namespace ArPaint.UI.Views.DrawingInfo
{
    public class DrawingInfoView : View<DrawingInfoViewModel>
    {
        public class Factory : PlaceholderFactory<DrawingInfoView> { }
    }
}