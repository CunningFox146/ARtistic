using System;
using System.Collections.Generic;

namespace ArPaint.UI.Systems.Stack
{
    public class ViewStack : IViewStack, IDisposable
    {
        private readonly IViewProvider _viewProvider;
        private readonly Stack<IStackableView> _viewStack = new();

        public IStackableView ActiveView => _viewStack.TryPeek(out var view) ? view : null;

        public ViewStack(IViewProvider viewProvider)
        {
            _viewProvider = viewProvider;
        }
        
        public void PushView<TView>() where TView : IStackableView
        {
            (ActiveView as IViewFocusable)?.OnUnfocus();

            var view = _viewProvider.GetView<TView>();

            if (view == null)
                return;

            _viewStack.Push(view);
            
            (view as ISortableView)?.SetSortOrder(_viewStack.Count);
            view.SetViewStack(this);
            view.Show();
        }

        public void PopView()
        {
            if (!_viewStack.TryPop(out var view))
                return;

            view.Show();
            
            (view as ISortableView)?.SetSortOrder(0);
            (ActiveView as IViewFocusable)?.OnFocus();
        }

        public void Dispose()
        {
            _viewStack.Clear();
        }
    }
}