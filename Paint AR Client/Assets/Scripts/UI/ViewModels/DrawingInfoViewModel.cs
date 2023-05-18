using System.Threading;
using ArPaint.Infrastructure.SceneManagement;
using ArPaint.Services.Draw;
using Cysharp.Threading.Tasks;
using Services.PreviewRenderer;
using Services.Screenshot;
using UnityEngine;
using UnityMvvmToolkit.Core;
using UnityMvvmToolkit.Core.Attributes;
using UnityMvvmToolkit.Core.Interfaces;
using UnityMvvmToolkit.UniTask;
using UnityMvvmToolkit.UniTask.Interfaces;

namespace ArPaint.UI.ViewModels
{
    public class DrawingInfoViewModel : ViewModel
    {
        private readonly IDrawingsProvider _drawingsProvider;
        private readonly ISceneLoader _sceneLoader;
        private readonly IPreviewRenderer _previewRenderer;
        private readonly IScreenshotService _screenshotService;
        private readonly RenderTexture _renderTexture;

        [Observable(nameof(DrawingName))]
        private readonly IProperty<string> _drawingName;

        [Observable(nameof(DrawingDescription))]
        private readonly IProperty<string> _drawingDescription;

        private DrawingData _selectedDrawing;

        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand CloseViewCommand { get; }
        public IAsyncCommand ScreenshotCommand { get; }
        public IAsyncCommand DrawCommand { get; }

        public string DrawingName
        {
            get => _drawingName.Value;
            set => _drawingName.Value = value;
        }
        
        public string DrawingDescription
        {
            get => _drawingDescription.Value;
            set => _drawingDescription.Value = value;
        }

        public DrawingInfoViewModel(IDrawingsProvider drawingsProvider, ISceneLoader sceneLoader, IPreviewRenderer previewRenderer, IScreenshotService screenshotService, RenderTexture renderTexture)
        {
            _drawingsProvider = drawingsProvider;
            _sceneLoader = sceneLoader;
            _previewRenderer = previewRenderer;
            _screenshotService = screenshotService;
            _renderTexture = renderTexture;

            _drawingName = new Property<string>();
            _drawingDescription = new Property<string>();

            CloseViewCommand = new Command(CloseView);
            SaveCommand = new Command(Save);
            DeleteCommand = new Command(Delete, () => _selectedDrawing != null);
            ScreenshotCommand = new AsyncCommand(Screenshot, ()=> _selectedDrawing is { DrawCommands: not null }) {DisableOnExecution = true};
            DrawCommand = new AsyncCommand(Draw) {DisableOnExecution = true};

            _drawingsProvider.SelectedDrawingChanged += OnSelectedDrawingChanged;
            OnSelectedDrawingChanged(_drawingsProvider.SelectedDrawing);
        }

        private async UniTask Screenshot(CancellationToken cancellationToken)
        {
            await _screenshotService.Screenshot(_renderTexture, cancellationToken);
        }

        private void Delete()
        {
            _drawingsProvider.RemoveData(_selectedDrawing);
            _drawingsProvider.Save();
            CloseView();
        }

        private void OnSelectedDrawingChanged(DrawingData drawing)
        {
            _selectedDrawing = drawing;
            DrawingName = _selectedDrawing?.Name;
            DrawingDescription = _selectedDrawing?.Description;
            
            if (_selectedDrawing is { DrawCommands: not null })
                _previewRenderer.RenderDrawing(_selectedDrawing.DrawCommands);
            else
                _previewRenderer.Clear();
            
            DeleteCommand.RaiseCanExecuteChanged();
            ScreenshotCommand.RaiseCanExecuteChanged();
        }

        private void CloseView()
        {
            ViewStack.PopView();
        }

        private void Save()
        {
            if (string.IsNullOrEmpty(DrawingName))
                return;
            
            _selectedDrawing ??= _drawingsProvider.CreateNewData();
            _selectedDrawing.Name = DrawingName;
            _selectedDrawing.Description = DrawingDescription;
            _drawingsProvider.SelectDrawing(_selectedDrawing, true);
            _drawingsProvider.Save();
            CloseView();
        }

        private async UniTask Draw(CancellationToken _)
        {
            if (string.IsNullOrEmpty(DrawingName))
                return;
            
            Save();
            await _sceneLoader.LoadScene(SceneIndex.Draw);
        }
    }
}