using ArPaint.Infrastructure.SceneManagement;
using Firebase;
using Firebase.Analytics;
using Services.Auth;
using Services.StaticData;
using UnityEngine;
using Zenject;

namespace ArPaint.Infrastructure.GameStates
{
    public class BootstrapState : IEnterState
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IStaticDataService _staticData;
        private readonly IAuthSystem _auth;

        public BootstrapState(ISceneLoader sceneLoader, IStaticDataService staticData, IAuthSystem auth)
        {
            _sceneLoader = sceneLoader;
            _staticData = staticData;
            _auth = auth;
        }

        public async void OnEnter()
        {
            Application.targetFrameRate = 300;
            await _staticData.Load();
            await _auth.Init();
            await _sceneLoader.LoadScene(SceneIndex.Draw);
            // await _sceneLoader.LoadScene(_auth.IsSignedIn ? SceneIndex.MainMenu : SceneIndex.Auth);
        }

        public class Factory : PlaceholderFactory<BootstrapState>
        {
        }
    }
}