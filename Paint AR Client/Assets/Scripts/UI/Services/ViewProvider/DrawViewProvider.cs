using System;
using System.Collections.Generic;
using ArPaint.Infrastructure;
using ArPaint.UI.Services.Stack;
using ArPaint.UI.Views.ArInit;
using ArPaint.UI.Views.Draw;
using ArPaint.UI.Views.DrawingPlacer;
using ArPaint.UI.Views.DrawOptions;

namespace UI.Systems.ViewProvider
{
    public class DrawViewProvider : ViewProviderBase
    {
        public DrawViewProvider(ArInitView.Factory arInitViewFactory, DrawingPlacerView.Factory drawingPlacerViewFactory,
            DrawView.Factory drawViewFactory, DrawOptionsView.Factory drawOptionsViewFactory)
        {
            views = new Dictionary<Type, IStackableView>
            {
                [typeof(ArInitView)] = arInitViewFactory.Create(),
                [typeof(DrawingPlacerView)] = drawingPlacerViewFactory.Create(),
                [typeof(DrawView)] = drawViewFactory.Create(),
                [typeof(DrawOptionsView)] = drawOptionsViewFactory.Create()
            };

            HideAllViews();
        }
    }
}