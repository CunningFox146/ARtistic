using ArPaint.Infrastructure.AssetProvider;
using ArPaint.UI.Systems.Stack;
using ArPaint.UI.ViewModels.Draw;
using ArPaint.UI.ViewModels.DrawOptions;
using ArPaint.UI.Views.Draw;
using ArPaint.UI.Views.DrawOptions;
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
            InstallDrawView();
            InstallDrawOptionsView();
            Container.Bind<IViewStack>().To<ViewStack>().AsSingle().NonLazy();
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