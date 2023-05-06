using System;
using System.Collections.Generic;
using ArPaint.UI.Views.Draw;
using ArPaint.UI.Views.DrawOptions;
using Zenject;

namespace ArPaint.UI.Systems.Stack
{
    public class ViewStack : IViewStack
    {
        private readonly Stack<IStackableView> _viewStack = new();
        private readonly Dictionary<Type, IFactory<IStackableView>> _viewFactories;

        public IStackableView ActiveView => _viewStack.TryPeek(out var view) ? view : null;

        public ViewStack(DrawView.Factory drawViewFactory, DrawOptionsView.Factory drawOptionsViewFactory)
        {
            _viewFactories = new()
            {
                [typeof(DrawView)] = drawViewFactory,
                [typeof(DrawOptionsView)] = drawOptionsViewFactory
            };
        }

        public void PushView<TView>() where TView : IStackableView
        {
            (ActiveView as IViewFocusable)?.OnUnfocus();

            var factory = _viewFactories[typeof(TView)];
            var view = factory.Create();
            _viewStack.Push(view);
        }

        public void PopView()
        {
            if (!_viewStack.TryPop(out var view))
                return;
            
            view.Destroy();
            (ActiveView as IViewFocusable)?.OnFocus();
        }
    }
}