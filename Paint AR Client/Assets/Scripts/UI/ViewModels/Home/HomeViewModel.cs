using System.Collections.ObjectModel;
using System.Collections.Specialized;
using ArPaint.Services.Draw;
using UnityMvvmToolkit.Core;
using UnityMvvmToolkit.Core.Attributes;
using UnityMvvmToolkit.Core.Interfaces;

namespace ArPaint.UI.ViewModels.Home
{
    public class HomeViewModel : ViewModel
    {
        private readonly IDrawingsProvider _drawingsProvider;

        [Observable(nameof(Drawings))]
        private readonly IReadOnlyProperty<ObservableCollection<DrawingViewModel>> _drawings;

        public ObservableCollection<DrawingViewModel> Drawings => _drawings.Value;
        
        public ICommand CreateDrawingCommand { get; }

        public HomeViewModel(IDrawingsProvider drawingsProvider)
        {
            _drawingsProvider = drawingsProvider;

            CreateDrawingCommand = new Command(CreateDrawing);
            
            _drawings = new Property<ObservableCollection<DrawingViewModel>>(new ObservableCollection<DrawingViewModel>());
            _drawingsProvider.Drawings.CollectionChanged += OnDrawingsChanged;
            BuildDrawingsCollection();
        }

        private void CreateDrawing()
        {
            throw new System.NotImplementedException();
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

        private void SelectDrawing(DrawingData drawing)
        {
            _drawingsProvider.SelectedDrawing = drawing;
        }
    }
}