using ArPaint.Infrastructure.GameStates;
using ArPaint.Services.Commands;
using ArPaint.Services.Input;
using UnityEngine.XR.ARFoundation;
using Zenject;

namespace ArPaint.Infrastructure
{
    public class DrawSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ARPlaneManager>().FromComponentInHierarchy().AsSingle();
            Container.Bind<ARAnchorManager>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<InputSource>().AsSingle().NonLazy();
            Container.Bind<ICommandBuffer>().To<CommandBuffer>().AsSingle();
            Container.BindFactory<ArInitState, ArInitState.Factory>();
            Container.BindFactory<DrawState, DrawState.Factory>();
            Container.BindInterfacesAndSelfTo<DrawSceneBootstrap>().AsSingle().NonLazy();
        }
    }
}