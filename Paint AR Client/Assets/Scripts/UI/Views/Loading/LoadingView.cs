using ArPaint.UI.ViewModels;
using ArPaint.UI.ViewModels.Loading;
using Zenject;

namespace ArPaint.UI.Views.Loading
{
    public class LoadingView : View<LoadingViewModel>
    {
        public class Factory : PlaceholderFactory<LoadingView>
        {
        }
    }
}