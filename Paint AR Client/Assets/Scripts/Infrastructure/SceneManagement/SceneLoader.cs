using System;
using ArPaint.UI.Systems.LoadingDisplay;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace ArPaint.Infrastructure.SceneManagement
{
    public class SceneLoader : ISceneLoader
    {
        private readonly ILoadingDisplaySystem _loadingDisplaySystem;

        public SceneLoader(ILoadingDisplaySystem loadingDisplaySystem)
        {
            _loadingDisplaySystem = loadingDisplaySystem;
        }
        
        public async UniTask LoadScene(SceneIndex scene, Action callback = null)
        {
            _loadingDisplaySystem.ShowLoadingView();
            await SceneManager.LoadSceneAsync((int)scene, LoadSceneMode.Single);
            callback?.Invoke();
        }
    }
}