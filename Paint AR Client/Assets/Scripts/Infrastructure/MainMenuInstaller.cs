using ArPaint.Infrastructure.AssetProvider;
using ArPaint.Infrastructure.GameStates;
using ArPaint.Services.Draw.Shapes;
using ArPaint.UI.Systems.Stack;
using ArPaint.UI.ViewModels;
using ArPaint.UI.ViewModels.Home;
using ArPaint.UI.ViewModels.ProfileView;
using ArPaint.UI.Views.DrawingInfo;
using ArPaint.UI.Views.Home;
using ArPaint.UI.Views.Profile;
using Services.PreviewRenderer;
using UI.Systems.ViewProvider;
using UnityEngine;
using Zenject;

namespace ArPaint.Infrastructure
{
    public class MainMenuInstaller : MonoInstaller
    {
        [SerializeField] private Camera _shapesCamera;
        [SerializeField] private GameObject _profileView;
        [SerializeField] private GameObject _homeView;
        [SerializeField] private GameObject _drawingInfoView;
        
        private IPrefabsProvider _prefabsProvider;

        [Inject]
        public void Constructor(IPrefabsProvider prefabsProvider)
        {
            _prefabsProvider = prefabsProvider;
        }
        
        public override void InstallBindings()
        {
            Container.Bind<Camera>().FromInstance(_shapesCamera).AsSingle();
            Container.Bind<RenderTexture>().FromInstance(_shapesCamera.targetTexture).AsSingle();
            Container.BindInterfacesAndSelfTo<ProfileViewModel>().AsSingle();
            Container.BindFactory<ProfileView, ProfileView.Factory>()
                .FromComponentInNewPrefab(_profileView);
            
            Container.BindInterfacesAndSelfTo<HomeViewModel>().AsSingle();
            Container.BindFactory<HomeView, HomeView.Factory>()
                .FromComponentInNewPrefab(_homeView);
            
            Container.BindInterfacesAndSelfTo<DrawingInfoViewModel>().AsSingle();
            Container.BindFactory<DrawingInfoView, DrawingInfoView.Factory>()
                .FromComponentInNewPrefab(_drawingInfoView);

            Container.BindInterfacesTo<MainMenuViewProvider>().AsSingle();
            Container.BindInterfacesTo<ViewStack>().AsSingle();

            Container.BindFactory<MainMenuState, MainMenuState.Factory>();

            Container.BindFactory<ShapeContainer, ShapeContainer.Factory>()
                .FromComponentInNewPrefab(_prefabsProvider.ShapeContainerPrefab);
            
            Container.BindInterfacesAndSelfTo<PreviewRenderer>().AsSingle();

            Container.BindInterfacesAndSelfTo<MainMenuBootstrap>().AsSingle().NonLazy();
        }
    }
}