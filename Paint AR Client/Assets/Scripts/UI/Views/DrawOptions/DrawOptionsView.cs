using ArPaint.UI.ViewModels.DrawOptions;
using Zenject;

namespace ArPaint.UI.Views.DrawOptions
{
    public class DrawOptionsView : View<DrawOptionsViewModel>
    {
        public class Factory : PlaceholderFactory<DrawOptionsView>
        {
        }
    }
}