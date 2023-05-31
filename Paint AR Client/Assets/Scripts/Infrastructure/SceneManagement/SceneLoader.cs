using System;
using ArPaint.UI.Services.LoadingDisplay;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace ArPaint.Infrastructure.SceneManagement
{
    public class SceneLoader : ISceneLoader
    {
        private readonly ILoadingDisplay loadingDisplay;

        public SceneLoader(ILoadingDisplay loadingDisplay)
        {
            this.loadingDisplay = loadingDisplay;
        }
        
        public async UniTask LoadScene(SceneIndex scene, Action callback = null)
        {
            loadingDisplay.ShowLoadingView();
            await SceneManager.LoadSceneAsync((int)scene, LoadSceneMode.Single);
            callback?.Invoke();
        }
    }
}