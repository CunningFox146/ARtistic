using ArPaint.Infrastructure.AssetProvider;
using ArPaint.Infrastructure.GameLoop;
using ArPaint.Infrastructure.GameStates;
using ArPaint.Infrastructure.SceneManagement;
using Firebase;
using Firebase.Auth;
using Google;
using Services.Auth;
using Services.StaticData;
using Services.Toast;
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
            Container.Bind<IAuthSystem>().To<AuthSystem>().AsSingle();
            Container.BindInterfacesTo<UpdateLoop>().FromComponentInHierarchy().AsSingle();
            Container.BindFactory<BootstrapState, BootstrapState.Factory>();
     
#if UNITY_EDITOR
            Container.Bind<IToast>().To<ToastEditor>().AsSingle();
#else
            Container.Bind<IToast>().To<ToastAndroid>().AsSingle();
            Container.Bind<GoogleSignIn>().FromMethod(_ => GoogleSignIn.DefaultInstance);
#endif
            Container.Bind<FirebaseApp>().FromMethod(_ => FirebaseApp.DefaultInstance);
            Container.Bind<FirebaseAuth>().FromMethod(_ => FirebaseAuth.DefaultInstance);
            
            Container.BindInterfacesAndSelfTo<GameBootstrap>().AsSingle().NonLazy();
        }
    }
}