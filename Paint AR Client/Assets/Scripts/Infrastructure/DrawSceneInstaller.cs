using ArPaint.Infrastructure.AssetProvider;
using ArPaint.Infrastructure.GameStates;
using ArPaint.Services.Commands;
using ArPaint.Services.Draw;
using ArPaint.Services.Draw.Shapes;
using ArPaint.Services.Input;
using ArPaint.UI.Systems.Stack;
using ArPaint.UI.ViewModels.ArInit;
using ArPaint.UI.ViewModels.Draw;
using ArPaint.UI.ViewModels.DrawOptions;
using ArPaint.UI.Views.ArInit;
using ArPaint.UI.Views.Draw;
using ArPaint.UI.Views.DrawOptions;
using UI.Systems.ViewProvider;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using Zenject;

namespace ArPaint.Infrastructure
{
    public class DrawSceneInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _arInitView;
        [SerializeField] private GameObject _drawView;
        [SerializeField] private GameObject _drawOptionsView;

        
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

            Container.BindInterfacesAndSelfTo<ArInitViewModel>().AsSingle();
            Container.BindFactory<ArInitView, ArInitView.Factory>()
                .FromComponentInNewPrefab(_arInitView);
            
            Container.BindInterfacesAndSelfTo<DrawViewModel>().AsSingle();
            Container.BindFactory<DrawView, DrawView.Factory>()
                .FromComponentInNewPrefab(_drawView);

            Container.BindInterfacesAndSelfTo<DrawOptionsViewModel>().AsSingle();
            Container.BindFactory<DrawOptionsView, DrawOptionsView.Factory>()
                .FromComponentInNewPrefab(_drawOptionsView);
            
            Container.BindInterfacesTo<ViewStack>().AsSingle();
            Container.BindInterfacesTo<DrawViewProvider>().AsSingle();
            
            Container.BindFactory<ArInitState, ArInitState.Factory>();
            Container.BindFactory<DrawState, DrawState.Factory>();
            Container.BindFactory<ShapeContainer, ShapeContainer.Factory>()
                .FromComponentInNewPrefab(_prefabsProvider.ShapeContainerPrefab);
            Container.BindInterfacesAndSelfTo<DrawSceneBootstrap>().AsSingle().NonLazy();
        }
    }
}