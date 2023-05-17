﻿using ArPaint.Infrastructure.GameStates;
using ArPaint.UI.Systems.Stack;
using ArPaint.UI.ViewModels.Home;
using ArPaint.UI.ViewModels.ProfileView;
using ArPaint.UI.Views.Home;
using ArPaint.UI.Views.Profile;
using UI.Systems.ViewProvider;
using UnityEngine;
using Zenject;

namespace ArPaint.Infrastructure
{
    public class MainMenuInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _profileView;
        [SerializeField] private GameObject _homeView;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ProfileViewModel>().AsSingle();
            Container.BindFactory<ProfileView, ProfileView.Factory>()
                .FromComponentInNewPrefab(_profileView);
            
            Container.BindInterfacesAndSelfTo<HomeViewModel>().AsSingle();
            Container.BindFactory<HomeView, HomeView.Factory>()
                .FromComponentInNewPrefab(_homeView);

            Container.BindInterfacesTo<MainMenuViewProvider>().AsSingle();
            Container.BindInterfacesTo<ViewStack>().AsSingle();

            Container.BindFactory<MainMenuState, MainMenuState.Factory>();

            Container.BindInterfacesAndSelfTo<MainMenuBootstrap>().AsSingle().NonLazy();
        }
    }
}