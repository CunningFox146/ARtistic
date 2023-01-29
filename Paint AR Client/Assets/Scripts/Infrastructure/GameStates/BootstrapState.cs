using ArPaint.Infrastructure.SceneManagement;
using UnityEngine;

namespace ArPaint.Infrastructure.GameStates
{
    public class BootstrapState : IState
    {
        private readonly IGameStateMachine _gameState;
        private readonly ISceneLoader _sceneLoader;

        public BootstrapState(IGameStateMachine gameState,  ISceneLoader sceneLoader)
        {
            _gameState = gameState;
            _sceneLoader = sceneLoader;
        }

        public void OnEnter()
        {
            Application.targetFrameRate = 300;
            _sceneLoader.LoadScene(SceneIndex.Draw);
            _gameState.EnterState<DrawState>();
        }

        public void OnExit()
        {
            
        }
    }
}