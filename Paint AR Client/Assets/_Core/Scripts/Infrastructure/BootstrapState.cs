using UnityEngine;

namespace ArPaint.Infrastructure
{
    public class BootstrapState : IState
    {
        private readonly ISceneLoader _sceneLoader;

        public BootstrapState(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }
        
        public void OnExit()
        {
            
        }

        public void OnEnter()
        {
            Debug.Log("WOW!");
        }
    }
}