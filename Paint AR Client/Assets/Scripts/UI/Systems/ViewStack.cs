using System;
using System.Collections.Generic;
using ArPaint.UI.Views.DrawOptions;
using Zenject;

namespace UI.Systems
{
    public class ViewStack : IViewStack
    {
        private readonly Stack<IStackableView> _viewStack = new();
        private readonly Dictionary<Type, IFactory<IStackableView>> _viewFactories;

        public IStackableView ActiveView => _viewStack.TryPeek(out var view) ? view : null;

        public ViewStack(DrawOptionsView.Factory drawViewFactory)
        {
            _viewFactories = new()
            {
                [typeof(DrawOptionsView)] = drawViewFactory
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