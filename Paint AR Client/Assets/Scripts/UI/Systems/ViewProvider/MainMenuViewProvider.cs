using System;
using System.Collections.Generic;
using ArPaint.Infrastructure;
using ArPaint.UI.Systems.Stack;
using ArPaint.UI.Views.Profile;

namespace UI.Systems.ViewProvider
{
    public class MainMenuViewProvider : ViewProviderBase
    {
        public MainMenuViewProvider(ProfileView.Factory profileViewFactory)
        {
            views = new Dictionary<Type, IStackableView>
            {
                [typeof(ProfileView)] = profileViewFactory.Create(),
            };

            HideAllViews();
        }
    }
}