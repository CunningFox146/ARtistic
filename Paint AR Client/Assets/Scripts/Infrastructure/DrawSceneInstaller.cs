using ArPaint.Infrastructure.AssetProvider;
using ArPaint.Infrastructure.GameStates;
using ArPaint.Services.Commands;
using ArPaint.Services.Draw;
using ArPaint.Services.Draw.Shapes;
using ArPaint.Services.Input;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using Zenject;

namespace ArPaint.Infrastructure
{
    public class DrawSceneInstaller : MonoInstaller
    {
        private IPrefabsProvider _prefabsProvider;

        [Inject]
        public void Constructor(IPrefabsProvider prefabsProvider)
        {
            _prefabsProvider = prefabsProvider;
        }

        public override void InstallBindings()
        {
            Container.Bind<Camera>().FromInstance(Camera.main).AsSingle();
            Container.Bind<ARPlaneManager>().FromComponentInHierarchy().AsSingle();
            Container.Bind<ICommandBuffer>().To<CommandBuffer>().AsSingle();
            Container.BindInterfacesAndSelfTo<InputSource>().AsSingle();
            Container.BindInterfacesAndSelfTo<DrawService>().AsSingle();
            
            Container.BindFactory<ArInitState, ArInitState.Factory>();
            Container.BindFactory<DrawState, DrawState.Factory>();
            Container.BindFactory<ShapeContainer, ShapeContainer.Factory>()
                .FromComponentInNewPrefab(_prefabsProvider.ShapeContainerPrefab);
            Container.BindInterfacesAndSelfTo<DrawSceneBootstrap>().AsSingle().NonLazy();
        }
    }
}