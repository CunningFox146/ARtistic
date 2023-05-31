using ArPaint.UI.Services.Stack;

namespace ArPaint.UI.Views
{
    public interface IViewHiddenHandler
    {
        void OnViewHidden(IStackableView view);
    }
}