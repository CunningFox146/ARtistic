using System;
using System.Collections.Generic;
using ArPaint.Infrastructure;
using ArPaint.UI.Systems.Stack;
using ArPaint.UI.Views.ArInit;
using ArPaint.UI.Views.Draw;
using ArPaint.UI.Views.DrawOptions;
using ArPaint.UI.Views.Register;

namespace UI.Systems.ViewProvider
{
    public class AuthViewProvider : ViewProviderBase
    {
        public AuthViewProvider(RegisterView.Factory registerViewFactory)
        {
            views = new Dictionary<Type, IStackableView>
            {
                [typeof(RegisterView)] = registerViewFactory.Create(),
            };

            HideAllViews();
        }
    }
}