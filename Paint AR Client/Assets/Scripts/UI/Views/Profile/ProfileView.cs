using ArPaint.UI.ViewModels.ProfileView;
using Zenject;

namespace ArPaint.UI.Views.Profile
{
    public class ProfileView : View<ProfileViewModel>
    {
        public class Factory : PlaceholderFactory<ProfileView>
        {
        }
    }
}