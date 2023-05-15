using ArPaint.Infrastructure.AssetProvider;
using ArPaint.UI.Systems.LoadingDisplay;
using ArPaint.UI.Systems.Stack;
using ArPaint.UI.ViewModels;
using ArPaint.UI.ViewModels.ArInit;
using ArPaint.UI.ViewModels.Draw;
using ArPaint.UI.ViewModels.DrawOptions;
using ArPaint.UI.ViewModels.Loading;
using ArPaint.UI.Views.ArInit;
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
            InstallFactories();
            
            Container.Bind<IViewProvider>().To<ViewProvider>().AsSingle();
            Container.Bind<IViewStack>().To<ViewStack>().AsSingle();

            InstallLoadingView();
            InstallArInitView();
            InstallDrawView();
            InstallDrawOptionsView();
        }

        private void InstallFactories()
        {
            Container.BindFactory<LoadingView, LoadingView.Factory>()
                .FromComponentInNewPrefab(_prefabsProvider.LoadViewPrefab(nameof(LoadingView)));

            Container.BindFactory<DrawView, DrawView.Factory>()
                .FromComponentInNewPrefab(_prefabsProvider.LoadViewPrefab(nameof(DrawView)));
            
            Container.BindFactory<ArInitView, ArInitView.Factory>()
                .FromComponentInNewPrefab(_prefabsProvider.LoadViewPrefab(nameof(ArInitView)));

            Container.BindFactory<DrawOptionsView, DrawOptionsView.Factory>()
                .FromComponentInNewPrefab(_prefabsProvider.LoadViewPrefab(nameof(DrawOptionsView)));
        }

        private void InstallLoadingView()
        {
            Container.BindInterfacesAndSelfTo<LoadingDisplaySystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<LoadingViewModel>().AsSingle();
        }

        private void InstallArInitView()
        {
            Container.BindInterfacesAndSelfTo<ArInitViewModel>().AsSingle();
        }

        private void InstallDrawView()
        {
            Container.BindInterfacesAndSelfTo<DrawViewModel>().AsSingle();
        }

        private void InstallDrawOptionsView()
        {
            Container.BindInterfacesAndSelfTo<DrawOptionsViewModel>().AsSingle();
        }
    }
}