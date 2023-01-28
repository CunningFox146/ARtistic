using System;
using ArPaint.Infrastructure.GameStates;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace ArPaint.Infrastructure.SceneManagement
{
    public class SceneLoader : ISceneLoader
    {
        public async UniTask LoadScene(SceneIndex scene, Action callback = null)
        {
            await SceneManager.LoadSceneAsync((int)scene, LoadSceneMode.Single);
            callback?.Invoke();
        }
    }
}