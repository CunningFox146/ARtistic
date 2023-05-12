using System;
using System.Collections.Generic;
using ArPaint.UI.Systems.Stack;
using ArPaint.UI.Views.Draw;
using ArPaint.UI.Views.DrawOptions;
using Zenject;

namespace ArPaint.Infrastructure
{
    public class ViewProvider : IViewProvider
    {
        private readonly Dictionary<Type, IStackableView> _views;

        public ViewProvider(DrawView.Factory drawViewFactory, DrawOptionsView.Factory drawOptionsViewFactory)
        {
            _views = new()
            {
                [typeof(DrawView)] = drawViewFactory.Create(),
                [typeof(DrawOptionsView)] = drawOptionsViewFactory.Create(),
            };

            foreach (var view in _views.Values)
            {
                view.Hide();
            }
        }

        public TView GetView<TView>() where TView : IStackableView
            => (TView)_views[typeof(TView)];
    }
}