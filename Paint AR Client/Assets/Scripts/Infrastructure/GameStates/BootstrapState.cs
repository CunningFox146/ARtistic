using ArPaint.Infrastructure.SceneManagement;
using UnityEngine;
using Zenject;

namespace ArPaint.Infrastructure.GameStates
{
    public class BootstrapState : IEnterState
    {
        private readonly ISceneLoader _sceneLoader;

        public BootstrapState(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public void OnEnter()
        {
            Application.targetFrameRate = 300;
            _sceneLoader.LoadScene(SceneIndex.Draw);
        }

        public class Factory : PlaceholderFactory<BootstrapState>
        {
        }
    }
}