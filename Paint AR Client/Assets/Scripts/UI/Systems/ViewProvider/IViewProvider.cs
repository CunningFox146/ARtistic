using ArPaint.UI.Systems.Stack;

namespace ArPaint.UI.Systems.ViewProvider
{
    public interface IViewProvider
    {
        TView GetView<TView>() where TView : IStackableView;
    }
}