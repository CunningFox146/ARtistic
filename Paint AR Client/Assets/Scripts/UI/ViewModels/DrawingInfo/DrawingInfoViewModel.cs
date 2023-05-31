using System;
using System.Threading;
using ArPaint.Infrastructure.SceneManagement;
using ArPaint.Services.Draw;
using Cysharp.Threading.Tasks;
using Services.PreviewRenderer;
using Services.Screenshot;
using Services.Toast;
using UnityEngine;
using UnityMvvmToolkit.Core;
using UnityMvvmToolkit.Core.Attributes;
using UnityMvvmToolkit.Core.Interfaces;
using UnityMvvmToolkit.UniTask;
using UnityMvvmToolkit.UniTask.Interfaces;

namespace ArPaint.UI.ViewModels.DrawingInfo
{
    public class DrawingInfoViewModel : ViewModel, IDisposable, INotifyViewInactive
    {
        [Observable(nameof(DrawingDescription))]
        private readonly IProperty<string> _drawingDescription;

        [Observable(nameof(DrawingName))]
        private readonly IProperty<string> _drawingName;
        
        [Observable] private readonly IProperty<string> _publishButtonText;
        [Observable] private readonly IProperty<string> _author;
        [Observable] private readonly IProperty<bool> _isOwned;
        [Observable] private readonly IProperty<bool> _hasDrawing;
        [Observable] private readonly IProperty<Quaternion> _previewRotation;

        private readonly IDrawingsProvider _drawingsProvider;
        private readonly IPreviewRenderer _previewRenderer;
        private readonly RenderTexture _renderTexture;
        private readonly IToast _toast;
        private readonly ISceneLoader _sceneLoader;
        private readonly IScreenshotService _screenshotService;

        private DrawingData _selectedDrawing;

        public ICommand CloseViewCommand { get; }
        public IAsyncCommand SaveCommand { get; }
        public IAsyncCommand DeleteCommand { get; }
        public IAsyncCommand ScreenshotCommand { get; }
        public IAsyncCommand PublishCommand { get; }
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

        public DrawingInfoViewModel(IDrawingsProvider drawingsProvider, ISceneLoader sceneLoader,
            IPreviewRenderer previewRenderer, IScreenshotService screenshotService, IToast toast)
        {
            _drawingsProvider = drawingsProvider;
            _sceneLoader = sceneLoader;
            _previewRenderer = previewRenderer;
            _screenshotService = screenshotService;
            _toast = toast;

            _drawingName = new Property<string>();
            _drawingDescription = new Property<string>();
            _publishButtonText = new Property<string>();
            _author = new Property<string>();
            _isOwned = new Property<bool>();
            _hasDrawing = new Property<bool>();
            _previewRotation = new Property<Quaternion>(new());
            _previewRenderer.SetRotationGetter(() => _previewRotation.Value);

            CloseViewCommand = new Command(CloseView);
            SaveCommand = new AsyncCommand(Save, () => _selectedDrawing != null) { DisableOnExecution = true };
            DeleteCommand = new AsyncCommand(Delete, () => _selectedDrawing != null) { DisableOnExecution = true };
            ScreenshotCommand = new AsyncCommand(Screenshot, () => _selectedDrawing is { DrawCommands: not null })
                { DisableOnExecution = true };
            DrawCommand = new AsyncCommand(Draw) { DisableOnExecution = true };
            PublishCommand = new AsyncCommand(Publish, () => _selectedDrawing != null) { DisableOnExecution = true };

            _drawingsProvider.SelectedDrawingChanged += OnSelectedDrawingChanged;
            OnSelectedDrawingChanged(_drawingsProvider.SelectedDrawing);
        }

        private async UniTask Publish(CancellationToken _)
        {
            if (_selectedDrawing.IsPublished)
                await _drawingsProvider.UnUploadDrawing(_selectedDrawing);
            else
            {
                await _drawingsProvider.UploadDrawing(_selectedDrawing);
            }

            _publishButtonText.Value = _selectedDrawing.IsPublished ? "Unpublish" : "Publish";
        }

        public void Dispose()
        {
            _drawingsProvider.SelectedDrawingChanged -= OnSelectedDrawingChanged;
        }

        private async UniTask Screenshot(CancellationToken cancellationToken)
        {
            await _screenshotService.Screenshot(_renderTexture, cancellationToken);
        }

        private async UniTask Delete(CancellationToken cancellationToken)
        {
            await _drawingsProvider.RemoveData(_selectedDrawing);
            await _drawingsProvider.Save();
            CloseView();
        }

        private void OnSelectedDrawingChanged(DrawingData drawing)
        {
            _selectedDrawing = drawing;
            DrawingName = _selectedDrawing?.Name;
            DrawingDescription = _selectedDrawing?.Description;
            _publishButtonText.Value = _selectedDrawing is { IsPublished: true } ? "Unpublish" : "Publish";
            _hasDrawing.Value = _selectedDrawing != null; 
            _author.Value = _selectedDrawing == null || _selectedDrawing.IsOwned ? "You" : _selectedDrawing.AuthorName;
            _isOwned.Value = _selectedDrawing == null || _selectedDrawing.IsOwned;

            if (_selectedDrawing is { DrawCommands: not null })
            {
                _previewRotation.Value = Quaternion.identity;
                _previewRenderer.RenderDrawing(_selectedDrawing.DrawCommands);
            }
            else
                _previewRenderer.Clear();

            SaveCommand.RaiseCanExecuteChanged();
            DeleteCommand.RaiseCanExecuteChanged();
            ScreenshotCommand.RaiseCanExecuteChanged();
            PublishCommand.RaiseCanExecuteChanged();
        }

        private void CloseView()
        {
            ViewStack.PopView();
        }

        private async UniTask Save(CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(DrawingName))
            {
                _toast.ShowMessage("Enter valid name");
                return;
            }

            _selectedDrawing ??= _drawingsProvider.CreateNewData();
            _selectedDrawing.Name = DrawingName;
            _selectedDrawing.Description = DrawingDescription;
            _drawingsProvider.UpdateDrawing(_selectedDrawing);
            _drawingsProvider.SelectDrawing(_selectedDrawing, true);
            await _drawingsProvider.Save();
            CloseView();
        }

        private async UniTask Draw(CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(DrawingName))
            {
                _toast.ShowMessage("Enter valid name");
                return;
            }

            await Save(cancellationToken);
            await _sceneLoader.LoadScene(SceneIndex.Draw);
        }

        public void OnViewInactive()
        {
            _previewRenderer.Clear();
        }
    }
}