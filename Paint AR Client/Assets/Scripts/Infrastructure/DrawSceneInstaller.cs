using ArPaint.Infrastructure.AssetProvider;
using ArPaint.Infrastructure.GameStates;
using ArPaint.Services.Commands;
using ArPaint.Services.Draw;
using ArPaint.Services.Input;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using Zenject;

namespace ArPaint.Infrastructure
{
    public class DrawSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Camera>().FromInstance(Camera.main).AsSingle();
            Container.Bind<ARPlaneManager>().FromComponentInHierarchy().AsSingle();
            Container.Bind<ICommandBuffer>().To<CommandBuffer>().AsSingle();
            // Container.Bind<IDrawableSource>().To<DrawableSource>().AsSingle();
            Container.BindInterfacesAndSelfTo<InputSource>().AsSingle();
            Container.BindInterfacesAndSelfTo<DrawService>().AsSingle();
            Container.BindFactory<ArInitState, ArInitState.Factory>();
            Container.BindFactory<DrawState, DrawState.Factory>();
            BindLineFactory();
            Container.BindInterfacesAndSelfTo<DrawSceneBootstrap>().AsSingle().NonLazy();
        }

        private void BindLineFactory()
        {
            var prefabsProvider = Container.Resolve<IPrefabsProvider>();
            Container.BindFactory<Line, Line.Factory>()
                .FromComponentInNewPrefab(prefabsProvider.LoadLinePrefab);
        }
    }
}