using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using ArPaint.Services.Draw;
using ArPaint.UI.ViewModels.Home;
using ArPaint.UI.ViewModels.MainMenu;
using ArPaint.UI.Views.DrawingInfo;
using Cysharp.Threading.Tasks;
using Services.Auth;
using UnityEngine;
using UnityMvvmToolkit.Core;
using UnityMvvmToolkit.Core.Attributes;
using UnityMvvmToolkit.Core.Interfaces;
using UnityMvvmToolkit.UniTask;
using UnityMvvmToolkit.UniTask.Interfaces;

namespace ArPaint.UI.ViewModels.Discover
{
    public class DiscoverViewModel : MainMenuViewModel
    {
        [Observable] private readonly IReadOnlyProperty<ObservableCollection<DrawingViewModel>> _drawings;
        
        private readonly IDrawingsProvider _drawingsProvider;
        private readonly IAuthSystem _authSystem;

        public IAsyncCommand UpdateDrawingsCommand;

        public DiscoverViewModel(IDrawingsProvider drawingsProvider, IAuthSystem authSystem)
        {
            _drawingsProvider = drawingsProvider;
            _authSystem = authSystem;
            _drawings = new ReadOnlyProperty<ObservableCollection<DrawingViewModel>>(new ());
            UpdateDrawingsCommand = new AsyncCommand(UpdateDrawings) { DisableOnExecution = true };
            UpdateDrawingsCommand.ExecuteAsync();
        }

        private async UniTask UpdateDrawings(CancellationToken _)
        {
            _drawings.Value.Clear();
            var drawings = await _drawingsProvider.GetPublishedDrawings();
            foreach (var drawing in drawings.Where(drawing => drawing.Author != _authSystem.User.UserId))
            {
                _drawings.Value.Add(new DrawingViewModel(drawing, SelectDrawing));
            }
        }

        private void SelectDrawing(DrawingData drawing)
        {
            _drawingsProvider.SelectDrawing(drawing);
            ViewStack.PushView<DrawingInfoView>();
        }
    }
}