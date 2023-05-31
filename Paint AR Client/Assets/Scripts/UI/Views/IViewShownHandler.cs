using ArPaint.UI.Services.Stack;

namespace ArPaint.UI.Views
{
    public interface IViewShownHandler
    {
        void OnViewShown(IStackableView view);
    }
}