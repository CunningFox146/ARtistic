namespace ArPaint.UI.Services.Stack
{
    public interface IViewStack
    {
        IStackableView ActiveView { get; }
        void PushView<TView>() where TView : IStackableView;
        void PopView();
    }
}