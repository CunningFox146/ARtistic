using ArPaint.Infrastructure.AssetProvider;
using ArPaint.Infrastructure.GameLoop;
using ArPaint.Infrastructure.GameStates;
using ArPaint.Infrastructure.SceneManagement;
using ArPaint.Services.Draw;
using ArPaint.UI.Systems.LoadingDisplay;
using ArPaint.UI.ViewModels.Loading;
using ArPaint.UI.Views.Loading;
using Firebase;
using Firebase.Auth;
using Services.Auth;
using Services.PersistentData;
using Services.StaticData;
using Services.Toast;
using UnityEngine;
using Zenject;

namespace ArPaint.Infrastructure
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _loadingView;

        public override void InstallBindings()
        {
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
            Container.Bind<IGameStateMachine>().To<GameStateMachine>().AsSingle();
            Container.Bind<IAssetProvider>().To<ResourcesAssetProvider>().AsSingle();
            Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();
            Container.Bind<IPrefabsProvider>().To<PrefabsProvider>().AsSingle();
            Container.Bind<IAuthSystem>().To<AuthSystem>().AsSingle();
            Container.Bind<IPersistentData>().To<PlayerPrefsPersistentData>().AsSingle();
            Container.Bind<IDrawingsProvider>().To<DrawingsProvider>().AsSingle();
            Container.BindInterfacesTo<UpdateLoop>().FromComponentInHierarchy().AsSingle();
            Container.BindFactory<BootstrapState, BootstrapState.Factory>();
            
#if UNITY_EDITOR
            Container.Bind<IToast>().To<ToastEditor>().AsSingle();
#else
            Container.Bind<IToast>().To<ToastAndroid>().AsSingle();
#endif
            Container.Bind<FirebaseApp>().FromMethod(_ => FirebaseApp.DefaultInstance);
            Container.Bind<FirebaseAuth>().FromMethod(_ => FirebaseAuth.DefaultInstance);
            
            
            Container.BindFactory<LoadingView, LoadingView.Factory>()
                .FromComponentInNewPrefab(_loadingView);
            Container.BindInterfacesAndSelfTo<LoadingViewModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<LoadingDisplaySystem>().AsSingle();

            Container.BindInterfacesAndSelfTo<GameBootstrap>().AsSingle().NonLazy();
        }
    }
}