using System.Threading;
using ArPaint.Infrastructure.GameStates;
using ArPaint.Infrastructure.SceneManagement;
using ArPaint.Services.Draw.Placer;
using Cysharp.Threading.Tasks;
using UnityMvvmToolkit.Core;
using UnityMvvmToolkit.Core.Interfaces;
using UnityMvvmToolkit.UniTask;
using UnityMvvmToolkit.UniTask.Interfaces;

namespace ArPaint.UI.ViewModels.DrawingPlacer
{
    public class DrawingPlacerViewModel : ViewModel
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly IDrawingPlacer _drawingPlacer;

        public IAsyncCommand CloseViewCommand { get; }
        public ICommand PlaceCommand { get; }

        public DrawingPlacerViewModel(IGameStateMachine stateMachine, ISceneLoader sceneLoader, IDrawingPlacer drawingPlacer)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _drawingPlacer = drawingPlacer;

            CloseViewCommand = new AsyncCommand(CloseView) { DisableOnExecution = true };
            PlaceCommand = new Command(Place, () => _drawingPlacer.IsPlacementValid);
            
            _drawingPlacer.PlacementValidChanged += OnPlacementValidChanged;
        }

        private void OnPlacementValidChanged(bool _)
        {
            PlaceCommand.RaiseCanExecuteChanged();
        }

        private async UniTask CloseView(CancellationToken _)
        {
            await _sceneLoader.LoadScene(SceneIndex.MainMenu);
        }

        private void Place()
        {
            _stateMachine.EnterState<DrawState>();
        }
    }
}