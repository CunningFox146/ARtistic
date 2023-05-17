using ArPaint.Infrastructure.GameStates;
using ArPaint.UI.Systems.Stack;
using ArPaint.UI.ViewModels.Register;
using ArPaint.UI.Views.Register;
using ArPaint.UI.Views.SignIn;
using UI.Systems.ViewProvider;
using UnityEngine;
using Zenject;

namespace ArPaint.Infrastructure
{
    public class AuthSceneInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _registerView;
        [SerializeField] private GameObject _singInView;
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<RegisterViewModel>().AsSingle();
            Container.BindFactory<RegisterView, RegisterView.Factory>()
                .FromComponentInNewPrefab(_registerView);
            
            Container.BindInterfacesAndSelfTo<SignInViewModel>().AsSingle();
            Container.BindFactory<SignInView, SignInView.Factory>()
                .FromComponentInNewPrefab(_singInView);
            
            Container.BindInterfacesTo<ViewStack>().AsSingle();
            Container.BindInterfacesTo<AuthViewProvider>().AsSingle();

            Container.BindFactory<AuthState, AuthState.Factory>();
            Container.BindInterfacesAndSelfTo<AuthSceneBootstrap>().AsSingle().NonLazy();
        }
    }
}