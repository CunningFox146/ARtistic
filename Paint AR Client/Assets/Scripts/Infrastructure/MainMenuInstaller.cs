using ArPaint.Infrastructure.GameStates;
using ArPaint.UI.Systems.Stack;
using ArPaint.UI.ViewModels.ProfileView;
using ArPaint.UI.Views.Profile;
using UI.Systems.ViewProvider;
using UnityEngine;
using Zenject;

namespace ArPaint.Infrastructure
{
    public class MainMenuInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _profileView;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ProfileViewModel>().AsSingle();
            Container.BindFactory<ProfileView, ProfileView.Factory>()
                .FromComponentInNewPrefab(_profileView);

            Container.BindInterfacesTo<MainMenuViewProvider>().AsSingle();
            Container.BindInterfacesTo<ViewStack>().AsSingle();

            Container.BindFactory<MainMenuState, MainMenuState.Factory>();

            Container.BindInterfacesAndSelfTo<MainMenuBootstrap>().AsSingle().NonLazy();
        }
    }
}