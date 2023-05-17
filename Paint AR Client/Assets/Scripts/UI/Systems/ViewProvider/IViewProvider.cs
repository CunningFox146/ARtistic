namespace ArPaint.UI.Systems.Stack
{
    public interface IViewProvider
    {
        TView GetView<TView>() where TView : IStackableView;
    }
}