using System.Threading;
using ArPaint.Infrastructure.SceneManagement;
using Cysharp.Threading.Tasks;
using UnityMvvmToolkit.UniTask;
using UnityMvvmToolkit.UniTask.Interfaces;

namespace ArPaint.UI.ViewModels.ArInit
{
    public class ArInitViewModel : ViewModel
    {
        private readonly ISceneLoader _sceneLoader;
        public IAsyncCommand CloseViewCommand { get; }

        public ArInitViewModel(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
            CloseViewCommand = new AsyncCommand(CloseView) { DisableOnExecution = true };
        }

        private async UniTask CloseView(CancellationToken _)
        {
            await _sceneLoader.LoadScene(SceneIndex.MainMenu);
        }
    }
}