using System;
using System.Collections.Generic;
using ArPaint.Infrastructure;
using ArPaint.UI.Systems.Stack;
using ArPaint.UI.Views.Discover;
using ArPaint.UI.Views.DrawingInfo;
using ArPaint.UI.Views.Home;
using ArPaint.UI.Views.Profile;

namespace UI.Systems.ViewProvider
{
    public class MainMenuViewProvider : ViewProviderBase
    {
        public MainMenuViewProvider(ProfileView.Factory profileViewFactory, HomeView.Factory homeViewFactory,
            DiscoverView.Factory discoverViewFactory,
            DrawingInfoView.Factory drawingInfoViewFactory)
        {
            views = new Dictionary<Type, IStackableView>
            {
                [typeof(ProfileView)] = profileViewFactory.Create(),
                [typeof(HomeView)] = homeViewFactory.Create(),
                [typeof(DiscoverView)] = discoverViewFactory.Create(),
                [typeof(DrawingInfoView)] = drawingInfoViewFactory.Create()
            };

            HideAllViews();
        }
    }
}