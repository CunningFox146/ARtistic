using ArPaint.Infrastructure.GameLoop;
using ArPaint.Infrastructure.GameStates;
using ArPaint.Infrastructure.SceneManagement;
using Zenject;

namespace ArPaint.Infrastructure
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
            Container.Bind<IGameStateMachine>().To<GameStateMachine>().AsSingle();
            Container.BindInterfacesTo<UpdateLoop>().FromComponentInHierarchy().AsSingle();
            Container.BindFactory<BootstrapState, BootstrapState.Factory>();
            Container.BindInterfacesAndSelfTo<GameBootstrap>().AsSingle().NonLazy();
        }
    }
}