using System;
using System.Collections.Generic;
using ArPaint.UI.Views.Draw;
using ArPaint.UI.Views.DrawOptions;
using ArPaint.UI.Views.Loading;
using Zenject;

namespace ArPaint.UI.Systems.Stack
{
    public class ViewStack : IViewStack
    {
        private readonly Dictionary<Type, IFactory<IStackableView>> _viewFactories;
        private readonly Stack<IStackableView> _viewStack = new();

        public IStackableView ActiveView => _viewStack.TryPeek(out var view) ? view : null;

        public ViewStack(DrawView.Factory drawViewFactory,
            DrawOptionsView.Factory drawOptionsViewFactory)
        {
            _viewFactories = new Dictionary<Type, IFactory<IStackableView>>
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