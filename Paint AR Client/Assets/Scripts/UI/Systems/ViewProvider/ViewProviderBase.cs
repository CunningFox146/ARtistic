using System;
using System.Collections.Generic;
using ArPaint.UI.Systems.Stack;
using ArPaint.UI.Views.ArInit;
using ArPaint.UI.Views.Draw;
using ArPaint.UI.Views.DrawOptions;
using ArPaint.UI.Views.Register;

namespace ArPaint.UI.Systems.ViewProvider
{
    public abstract class ViewProviderBase : IViewProvider, IDisposable
    {
        protected Dictionary<Type, IStackableView> views;

        protected void HideViews()
        {
            foreach (var view in views.Values) view.Hide();
        }

        public TView GetView<TView>() where TView : IStackableView
        {
            return (TView)views[typeof(TView)];
        }

        public void Dispose()
        {
            foreach (var view in views.Values)
            {
                view.Destroy();
            }
            views.Clear();
        }
    }
}