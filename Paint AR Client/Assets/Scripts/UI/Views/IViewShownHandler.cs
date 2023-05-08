using ArPaint.UI.Systems.Stack;

namespace ArPaint.UI.Views
{
    public interface IViewShownHandler
    {
        void OnViewShown(IStackableView view);
    }
}