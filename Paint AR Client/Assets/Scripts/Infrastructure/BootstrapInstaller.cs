using ArPaint.Infrastructure.AssetProvider;
using ArPaint.Infrastructure.GameLoop;
using ArPaint.Infrastructure.GameStates;
using ArPaint.Infrastructure.SceneManagement;
using Services.StaticData;
using Zenject;

namespace ArPaint.Infrastructure
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
            Container.Bind<IGameStateMachine>().To<GameStateMachine>().AsSingle();
            Container.Bind<IAssetProvider>().To<ResourcesAssetProvider>().AsSingle();
            Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();
            Container.Bind<IPrefabsProvider>().To<PrefabsProvider>().AsSingle();
            Container.BindInterfacesTo<UpdateLoop>().FromComponentInHierarchy().AsSingle();
            Container.BindFactory<BootstrapState, BootstrapState.Factory>();
            Container.BindInterfacesAndSelfTo<GameBootstrap>().AsSingle().NonLazy();
        }
    }
}