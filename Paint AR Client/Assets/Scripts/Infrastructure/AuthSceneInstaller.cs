using ArPaint.Infrastructure.GameStates;
using ArPaint.UI.Systems.Stack;
using ArPaint.UI.ViewModels.Register;
using ArPaint.UI.Views.Register;
using UI.Systems.ViewProvider;
using UnityEngine;
using Zenject;

namespace ArPaint.Infrastructure
{
    public class AuthSceneInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _registerView;
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<RegisterViewModel>().AsSingle();
            Container.BindFactory<RegisterView, RegisterView.Factory>()
                .FromComponentInNewPrefab(_registerView);
            
            Container.BindInterfacesTo<ViewStack>().AsSingle();
            Container.BindInterfacesTo<AuthViewProvider>().AsSingle();

            Container.BindFactory<AuthState, AuthState.Factory>();
            Container.BindInterfacesAndSelfTo<AuthSceneBootstrap>().AsSingle().NonLazy();
        }
    }
}