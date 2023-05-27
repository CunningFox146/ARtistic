using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
        [Observable] private readonly IProperty<string> _searchText;

        private readonly IAuthSystem _authSystem;
        private readonly IDrawingsProvider _drawingsProvider;

        public IAsyncCommand ClearSearchCommand { get; }
        public IAsyncCommand SearchDrawingsCommand { get; }

        public DiscoverViewModel(IDrawingsProvider drawingsProvider, IAuthSystem authSystem)
        {
            _drawingsProvider = drawingsProvider;
            _authSystem = authSystem;

            _searchText = new Property<string>();
            _drawings = new ReadOnlyProperty<ObservableCollection<DrawingViewModel>>(
                new ObservableCollection<DrawingViewModel>());

            ClearSearchCommand = new AsyncCommand(ClearSearch) { DisableOnExecution = true };
            SearchDrawingsCommand = new AsyncCommand(SearchDrawings) { DisableOnExecution = true };
            SearchDrawingsCommand.ExecuteAsync();
        }

        private async UniTask ClearSearch(CancellationToken cancellationToken)
        {
            _searchText.Value = string.Empty;
            await SearchDrawings(default);
        }

        private async UniTask SearchDrawings(CancellationToken _)
        {
            await SearchDrawingsInternal(_searchText.Value);
        }

        private async Task SearchDrawingsInternal(string search)
        {
            _drawings.Value.Clear();
            var drawings = await _drawingsProvider.GetPublishedDrawings();
            var foundDrawings = drawings.Where(drawing => drawing.Author != _authSystem.User.UserId);
            var sortedDrawings = string.IsNullOrWhiteSpace(search)
                ? foundDrawings
                : foundDrawings.Where(drawing =>
                    drawing.Name.ToLower().Contains(search.ToLower()));
            
            foreach (var drawing in sortedDrawings)
                _drawings.Value.Add(new DrawingViewModel(drawing, SelectDrawing));
        }

        private void SelectDrawing(DrawingData drawing)
        {
            _drawingsProvider.SelectDrawing(drawing);
            ViewStack.PushView<DrawingInfoView>();
        }
    }
}