using System;
using System.Collections.Generic;
using ArPaint.Infrastructure;
using ArPaint.UI.Systems.Stack;
using ArPaint.UI.Views.ArInit;
using ArPaint.UI.Views.Draw;
using ArPaint.UI.Views.DrawOptions;
using ArPaint.UI.Views.Register;
using ArPaint.UI.Views.SignIn;

namespace UI.Systems.ViewProvider
{
    public class AuthViewProvider : ViewProviderBase
    {
        public AuthViewProvider(RegisterView.Factory registerViewFactory, SignInView.Factory signInFactory)
        {
            views = new Dictionary<Type, IStackableView>
            {
                [typeof(RegisterView)] = registerViewFactory.Create(),
                [typeof(SignInView)] = signInFactory.Create(),
            };

            HideAllViews();
        }
    }
}