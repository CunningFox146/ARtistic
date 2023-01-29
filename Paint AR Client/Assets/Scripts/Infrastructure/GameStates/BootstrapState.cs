using ArPaint.Infrastructure.SceneManagement;
using UnityEngine;
using Zenject;

namespace ArPaint.Infrastructure.GameStates
{
    public class BootstrapState : IState
    {
        private readonly IGameStateMachine _gameState;
        private readonly ISceneLoader _sceneLoader;

        public BootstrapState(ISceneLoader sceneLoader, IGameStateMachine gameState)
        {
            _gameState = gameState;
            _sceneLoader = sceneLoader;
        }

        public void OnEnter()
        {
            Application.targetFrameRate = 300;
            _sceneLoader.LoadScene(SceneIndex.Draw);
        }

        public void OnExit()
        {
        }

        public class Factory : PlaceholderFactory<BootstrapState>
        {
        }
    }
}