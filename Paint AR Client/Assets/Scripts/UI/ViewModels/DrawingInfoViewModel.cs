using ArPaint.Infrastructure.SceneManagement;
using ArPaint.Services.Draw;
using UnityMvvmToolkit.Core;
using UnityMvvmToolkit.Core.Attributes;
using UnityMvvmToolkit.Core.Interfaces;

namespace ArPaint.UI.ViewModels
{
    public class DrawingInfoViewModel : ViewModel
    {
        private readonly IDrawingsProvider _drawingsProvider;
        private readonly ISceneLoader _sceneLoader;

        [Observable(nameof(DrawingName))]
        private readonly IProperty<string> _drawingName;

        [Observable(nameof(DrawingDescription))]
        private readonly IProperty<string> _drawingDescription;

        private DrawingData _selectedDrawing;

        public ICommand SaveCommand { get; }
        public ICommand DrawCommand { get; }
        public ICommand CloseViewCommand { get; }
        
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

        public DrawingInfoViewModel(IDrawingsProvider drawingsProvider, ISceneLoader sceneLoader)
        {
            _drawingsProvider = drawingsProvider;
            _sceneLoader = sceneLoader;

            _drawingName = new Property<string>();
            _drawingDescription = new Property<string>();

            CloseViewCommand = new Command(CloseView);
            SaveCommand = new Command(Save);
            DrawCommand = new Command(Draw);

            _drawingsProvider.SelectedDrawingChanged += OnSelectedDrawingChanged;
            OnSelectedDrawingChanged(_drawingsProvider.SelectedDrawing);
        }

        private void OnSelectedDrawingChanged(DrawingData drawing)
        {
            _selectedDrawing = drawing;
            if (_selectedDrawing != null)
            {
                DrawingName = _selectedDrawing.Name;
                DrawingDescription = _selectedDrawing.Description;
            }
        }

        private void CloseView()
        {
            ViewStack.PopView();
        }

        public void Save()
        {
            if (string.IsNullOrEmpty(DrawingName))
                return;
            
            _selectedDrawing ??= _drawingsProvider.CreateNewData();
            _selectedDrawing.Name = DrawingName;
            _selectedDrawing.Description = DrawingDescription;
            _drawingsProvider.SelectDrawing(_selectedDrawing, true);
        }

        public void Draw()
        {
            if (string.IsNullOrEmpty(DrawingName))
                return;
            
            Save();
            _sceneLoader.LoadScene(SceneIndex.Draw);
        }
    }
}