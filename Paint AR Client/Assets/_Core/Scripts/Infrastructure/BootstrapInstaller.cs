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
            Container.Bind<IGameStateMachine>().To<GameStateMachineMachine>().AsSingle().NonLazy();
        }
    }
}