using System;
using System.Collections.Generic;
using ArPaint.UI.Systems.Stack;
using ArPaint.UI.Systems.ViewProvider;
using ArPaint.UI.Views.ArInit;
using ArPaint.UI.Views.Draw;
using ArPaint.UI.Views.DrawOptions;
using ArPaint.UI.Views.Register;

namespace ArPaint.Infrastructure
{
    public class ViewProvider : IViewProvider
    {
        private readonly Dictionary<Type, IStackableView> _views;

        public ViewProvider(ArInitView.Factory arInitViewFactory, RegisterView.Factory registerViewFactory,
            DrawView.Factory drawViewFactory, DrawOptionsView.Factory drawOptionsViewFactory)
        {
            _views = new Dictionary<Type, IStackableView>
            {
                [typeof(RegisterView)] = registerViewFactory.Create(),
                [typeof(ArInitView)] = arInitViewFactory.Create(),
                [typeof(DrawView)] = drawViewFactory.Create(),
                [typeof(DrawOptionsView)] = drawOptionsViewFactory.Create()
            };

            foreach (var view in _views.Values) view.Hide();
        }

        public TView GetView<TView>() where TView : IStackableView
        {
            return (TView)_views[typeof(TView)];
        }
    }
}