using ArPaint.Infrastructure.AssetProvider;
using ArPaint.UI.ViewModels.Draw;
using ArPaint.UI.Views.Draw;
using UI.Systems;
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
            Container.Bind<IViewStack>().To<ViewStack>().AsSingle().NonLazy();
        }

        private void InstallDrawView()
        {
            Container.BindInterfacesAndSelfTo<DrawViewModel>().AsSingle();
            Container.BindFactory<DrawView, DrawView.Factory>()
                .FromComponentInNewPrefab(_prefabsProvider.DrawViewPrefab);
        }
    }
}