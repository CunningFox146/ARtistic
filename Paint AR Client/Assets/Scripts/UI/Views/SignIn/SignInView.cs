using ArPaint.UI.ViewModels.Register;
using ArPaint.UI.Views.Register;
using Zenject;

namespace ArPaint.UI.Views.SignIn
{
    public class SignInView : View<SignInViewModel>
    {
        public class Factory : PlaceholderFactory<SignInView>{}
    }
}