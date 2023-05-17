using System;
using System.Collections.Generic;
using ArPaint.UI.Systems.Stack;

namespace ArPaint.Infrastructure
{
    public abstract class ViewProviderBase : IViewProvider, IDisposable
    {
        protected Dictionary<Type, IStackableView> views;

        protected void HideAllViews()
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