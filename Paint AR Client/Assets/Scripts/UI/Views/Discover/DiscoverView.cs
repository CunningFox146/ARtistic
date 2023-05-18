using ArPaint.UI.ViewModels.Discover;
using Zenject;

namespace ArPaint.UI.Views.Discover
{
    public class DiscoverView : View<DiscoverViewModel>
    {
        public class Factory : PlaceholderFactory<DiscoverView> {}
    }
}