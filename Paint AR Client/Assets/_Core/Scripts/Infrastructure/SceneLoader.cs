using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace ArPaint.Infrastructure
{
    public class SceneLoader : ISceneLoader
    {
        public async UniTask LoadScene(SceneIndex scene, Action callback = null)
        {
            await SceneManager.LoadSceneAsync((int)scene);
            callback?.Invoke();
        }
    }
}