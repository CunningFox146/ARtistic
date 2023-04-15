namespace UI.Systems
{
    public interface IViewStack
    {
        IStackableView ActiveView { get; }
        void PushView<TView>() where TView : IStackableView;
        void PopView();
    }
}