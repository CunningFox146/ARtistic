using ArPaint.Infrastructure.GameStates;
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
            Container.BindFactory<ArInitState, ArInitState.Factory>();
            Container.BindInterfacesAndSelfTo<DrawSceneBootstrap>().AsSingle().NonLazy();
        }
    }
}