using System;
using System.Collections.Generic;
using ArPaint.Infrastructure;
using ArPaint.UI.Systems.Stack;
using ArPaint.UI.Views.ArInit;
using ArPaint.UI.Views.Draw;
using ArPaint.UI.Views.DrawOptions;

namespace UI.Systems.ViewProvider
{
    public class DrawViewProvider : ViewProviderBase
    {
        public DrawViewProvider(ArInitView.Factory arInitViewFactory,
            DrawView.Factory drawViewFactory, DrawOptionsView.Factory drawOptionsViewFactory)
        {
            views = new Dictionary<Type, IStackableView>
            {
                [typeof(ArInitView)] = arInitViewFactory.Create(),
                [typeof(DrawView)] = drawViewFactory.Create(),
                [typeof(DrawOptionsView)] = drawOptionsViewFactory.Create()
            };

            HideAllViews();
        }
    }
}