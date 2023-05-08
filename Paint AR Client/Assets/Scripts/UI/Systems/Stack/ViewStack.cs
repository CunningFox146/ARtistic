using System;
using System.Collections.Generic;
using ArPaint.UI.Views.Draw;
using ArPaint.UI.Views.DrawOptions;

namespace ArPaint.UI.Systems.Stack
{
    public class ViewStack : IViewStack, IDisposable
    {
        private readonly Dictionary<Type, IStackableView> _viewsPool;
        private readonly Stack<IStackableView> _viewStack = new();

        public IStackableView ActiveView => _viewStack.TryPeek(out var view) ? view : null;

        public ViewStack(DrawView.Factory drawViewFactory,
            DrawOptionsView.Factory drawOptionsViewFactory)
        {
            _viewsPool = new Dictionary<Type, IStackableView>
            {
                [typeof(DrawView)] = Create(drawViewFactory),
                [typeof(DrawOptionsView)] = drawOptionsViewFactory.Create()
            };
            
            foreach (var view in _viewsPool.Values)
            {
                view.Hide();
            }
        }

        private static DrawView Create(DrawView.Factory drawViewFactory)
        {
            return drawViewFactory.Create();
        }

        public void PushView<TView>() where TView : IStackableView
        {
            (ActiveView as IViewFocusable)?.OnUnfocus();

            var view = _viewsPool[typeof(TView)];
            view.Show();
            
            _viewStack.Push(view);
        }

        public void PopView()
        {
            if (!_viewStack.TryPop(out var view))
                return;

            view.Show();
            (ActiveView as IViewFocusable)?.OnFocus();
        }

        public void Dispose()
        {
            _viewStack.Clear();
            foreach (var view in _viewsPool.Values)
            {
                view.Destroy();
            }
            _viewsPool.Clear();
        }
    }
}