using ArPaint.UI.ViewModels.Register;
using Zenject;

namespace ArPaint.UI.Views.Register
{
    public class RegisterView : View<RegisterViewModel>
    {
        public class Factory : PlaceholderFactory<RegisterView>{}
    }
}