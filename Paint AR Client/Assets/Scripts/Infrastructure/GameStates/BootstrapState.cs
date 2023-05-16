using ArPaint.Infrastructure.SceneManagement;
using Firebase;
using Firebase.Analytics;
using Services.StaticData;
using UnityEngine;
using Zenject;

namespace ArPaint.Infrastructure.GameStates
{
    public class BootstrapState : IEnterState
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IStaticDataService _staticData;

        public BootstrapState(ISceneLoader sceneLoader, IStaticDataService staticData)
        {
            _sceneLoader = sceneLoader;
            _staticData = staticData;
        }

        public async void OnEnter()
        {
            Application.targetFrameRate = 300;
            await _staticData.Load();
            await _sceneLoader.LoadScene(SceneIndex.Draw);
            var status = await FirebaseApp.CheckDependenciesAsync();
            UnityEngine.Debug.Log($"Firebase status: {status}");
            FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
        }

        public class Factory : PlaceholderFactory<BootstrapState>
        {
        }
    }
}