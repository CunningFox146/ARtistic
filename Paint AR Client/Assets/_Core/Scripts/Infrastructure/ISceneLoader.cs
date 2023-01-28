using System;
using Cysharp.Threading.Tasks;

namespace ArPaint.Infrastructure
{
    public interface ISceneLoader
    {
        UniTask LoadScene(SceneIndex scene, Action callback = null);
    }
}