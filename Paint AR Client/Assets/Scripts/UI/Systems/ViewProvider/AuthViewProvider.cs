using System;
using System.Collections.Generic;
using ArPaint.UI.Systems.Stack;
using ArPaint.UI.Views.Register;

namespace ArPaint.UI.Systems.ViewProvider
{
    public class AuthViewProvider : ViewProviderBase
    {
        public AuthViewProvider(RegisterView.Factory registerViewFactory)
        {
            views = new Dictionary<Type, IStackableView>()
            {
                [typeof(RegisterView)] = registerViewFactory.Create(),
            };
            HideViews();
        }
    }
}