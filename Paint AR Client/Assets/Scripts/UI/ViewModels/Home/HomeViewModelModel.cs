using System.Collections.ObjectModel;
using System.Collections.Specialized;
using ArPaint.Services.Draw;
using ArPaint.UI.Systems.Stack;
using ArPaint.UI.ViewModels.MainMenu;
using ArPaint.UI.Views.DrawingInfo;
using UnityMvvmToolkit.Core;
using UnityMvvmToolkit.Core.Attributes;
using UnityMvvmToolkit.Core.Interfaces;

namespace ArPaint.UI.ViewModels.Home
{
    public class HomeViewModelModel : MainMenuViewModel
    {
        private readonly IDrawingsProvider _drawingsProvider;

        [Observable(nameof(Drawings))]
        private readonly IReadOnlyProperty<ObservableCollection<DrawingViewModel>> _drawings;

        public ObservableCollection<DrawingViewModel> Drawings => _drawings.Value;
        
        public ICommand CreateDrawingCommand { get; }

        public HomeViewModelModel(IDrawingsProvider drawingsProvider)
        {
            _drawingsProvider = drawingsProvider;

            CreateDrawingCommand = new Command(CreateDrawing);
            
            _drawings = new ReadOnlyProperty<ObservableCollection<DrawingViewModel>>(new ObservableCollection<DrawingViewModel>());
            _drawingsProvider.Drawings.CollectionChanged += OnDrawingsChanged;
            BuildDrawingsCollection();
        }

        private void OnDrawingsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            BuildDrawingsCollection();
        }

        private void BuildDrawingsCollection()
        {
            Drawings.Clear();
            foreach (var drawing in _drawingsProvider.Drawings)
            {
                Drawings.Add(new DrawingViewModel(drawing, SelectDrawing));
            }
        }

        private void CreateDrawing()
        {
            _drawingsProvider.SelectDrawing(null);
            ViewStack.PushView<DrawingInfoView>();
        }

        private void SelectDrawing(DrawingData drawing)
        {
            _drawingsProvider.SelectDrawing(drawing);
            ViewStack.PushView<DrawingInfoView>();
        }
    }
}