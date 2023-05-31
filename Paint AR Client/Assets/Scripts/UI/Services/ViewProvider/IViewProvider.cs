namespace ArPaint.UI.Services.Stack
{
    public interface IViewProvider
    {
        TView GetView<TView>() where TView : IStackableView;
    }
}