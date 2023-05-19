using ArPaint.UI.Views.Discover;
using ArPaint.UI.Views.Home;
using UnityMvvmToolkit.Core;
using ICommand = UnityMvvmToolkit.Core.Interfaces.ICommand;

namespace ArPaint.UI.ViewModels.MainMenu
{
    public class MainMenuViewModel : ViewModel
    {
        public ICommand OpenProfileTabCommand { get; }
        public ICommand OpenHomeTabCommand { get; }
        public ICommand OpenDiscoverTabCommand { get; }

        public MainMenuViewModel()
        {
            OpenProfileTabCommand = new Command(OpenProfileTab);
            OpenHomeTabCommand = new Command(OpenHomeTab);
            OpenDiscoverTabCommand = new Command(OpenDiscoverTab);
        }

        private void OpenProfileTab()
        {
            ViewStack.PopView();
            ViewStack.PushView<Views.Profile.ProfileView>();
        }

        private void OpenHomeTab()
        {
            ViewStack.PopView();
            ViewStack.PushView<HomeView>();
        }

        private void OpenDiscoverTab()
        {
            ViewStack.PopView();
            ViewStack.PushView<DiscoverView>();
        }
    }
}