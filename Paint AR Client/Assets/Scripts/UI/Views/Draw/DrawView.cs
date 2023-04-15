using ArPaint.UI.ViewModels.Draw;
using Zenject;

namespace ArPaint.UI.Views.Draw
{
    public class DrawView : View<DrawViewModel>
    {
        
        [Inject]
        public void Constructor(DrawViewModel viewModel)
        {
            ViewModel = viewModel;
        }
        
        public class Factory : PlaceholderFactory<DrawView> { }
    }
}