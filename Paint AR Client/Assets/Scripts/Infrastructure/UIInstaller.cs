using ArPaint.Infrastructure.AssetProvider;
using ArPaint.UI.Systems.Stack;
using ArPaint.UI.ViewModels;
using ArPaint.UI.ViewModels.Draw;
using ArPaint.UI.ViewModels.DrawOptions;
using ArPaint.UI.ViewModels.Loading;
using ArPaint.UI.Views.Draw;
using ArPaint.UI.Views.DrawOptions;
using ArPaint.UI.Views.Loading;
using Zenject;

namespace ArPaint.Infrastructure
{
    public class UIInstaller : MonoInstaller
    {
        private IPrefabsProvider _prefabsProvider;

        [Inject]
        public void Constructor(IPrefabsProvider prefabsProvider)
        {
            _prefabsProvider = prefabsProvider;
        }

        public override void InstallBindings()
        {
            InstallLoadingView();
            InstallDrawView();
            InstallDrawOptionsView();
            Container.Bind<IViewStack>().To<ViewStack>().AsSingle().NonLazy();
        }

        private void InstallLoadingView()
        {
            Container.BindInterfacesAndSelfTo<LoadingViewModel>().AsSingle();
            Container.BindFactory<LoadingView, LoadingView.Factory>()
                .FromComponentInNewPrefab(_prefabsProvider.LoadViewPrefab(nameof(LoadingView)));
        }

        private void InstallDrawView()
        {
            Container.BindInterfacesAndSelfTo<DrawViewModel>().AsSingle();
            Container.BindFactory<DrawView, DrawView.Factory>()
                .FromComponentInNewPrefab(_prefabsProvider.LoadViewPrefab(nameof(DrawView)));
        }

        private void InstallDrawOptionsView()
        {
            Container.BindInterfacesAndSelfTo<DrawOptionsViewModel>().AsSingle();
            Container.BindFactory<DrawOptionsView, DrawOptionsView.Factory>()
                .FromComponentInNewPrefab(_prefabsProvider.LoadViewPrefab(nameof(DrawOptionsView)));
        }
    }
}