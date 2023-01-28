using ArPaint.Infrastructure.SceneManagement;

namespace ArPaint.Infrastructure.GameStates
{
    public class BootstrapState : IState
    {
        private readonly ISceneLoader _sceneLoader;

        public BootstrapState(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public void OnEnter()
        {
            _sceneLoader.LoadScene(SceneIndex.Draw);
        }

        public void OnExit()
        {
        }
    }
}