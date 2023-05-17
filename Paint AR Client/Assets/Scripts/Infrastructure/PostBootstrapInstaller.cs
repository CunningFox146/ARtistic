using ArPaint.Infrastructure.GameStates;
using Firebase;
using Firebase.Auth;
using Zenject;

namespace ArPaint.Infrastructure
{
    public class PostBootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<FirebaseApp>().FromMethod(_ => FirebaseApp.DefaultInstance);
            Container.Bind<FirebaseAuth>().FromMethod(_ => FirebaseAuth.DefaultInstance);
            Container.BindFactory<PostBootstrapState, PostBootstrapState.Factory>();
            Container.BindInterfacesAndSelfTo<PostBootstrapBootstrap>().AsSingle().NonLazy();
        }
    }
}