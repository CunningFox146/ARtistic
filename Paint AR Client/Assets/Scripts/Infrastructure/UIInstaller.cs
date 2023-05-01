using ArPaint.Infrastructure.AssetProvider;
using ArPaint.UI.Systems.Stack;
using ArPaint.UI.ViewModels.DrawOptions;
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
            InstallDrawOptionsView();
            Container.Bind<IViewStack>().To<ViewStack>().AsSingle().NonLazy();
        }

        private void InstallDrawOptionsView()
        {
            Container.BindInterfacesAndSelfTo<DrawOptionsViewModel>().AsSingle();
            Container.BindFactory<DrawOptionsView, DrawOptionsView.Factory>()
                .FromComponentInNewPrefab(_prefabsProvider.LoadViewPrefab(nameof(DrawOptionsView)));
        }
    }
}