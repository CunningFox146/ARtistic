using System;
using ArPaint.Services.Draw;
using ArPaint.Utils;
using Cysharp.Threading.Tasks;
using Services.ImageProvider;
using UnityEngine;
using UnityMvvmToolkit.Common.Interfaces;
using UnityMvvmToolkit.Core;
using UnityMvvmToolkit.Core.Attributes;
using UnityMvvmToolkit.Core.Interfaces;

namespace ArPaint.UI.ViewModels.Home
{
    public class DrawingViewModel : ICollectionItem
    {

        [Observable(nameof(DrawingName))]
        private readonly IProperty<string> _drawingName;

        [Observable(nameof(DrawingDescription))]
        private readonly IProperty<string> _drawingDescription;
        
        [Observable]
        private readonly IProperty<Texture2D> _preview;

        private readonly Action<DrawingData> _selectDrawing;
        private readonly IImageProvider _imageProvider;

        public int Id { get; }
        public DrawingData Drawing { get; }
        public ICommand SelectDrawingCommand { get; }
        
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
        
        public DrawingViewModel(DrawingData drawing, Action<DrawingData> selectDrawing, IImageProvider imageProvider)
        {
            Drawing = drawing;
            Id = Guid.NewGuid().GetHashCode();
            
            _selectDrawing = selectDrawing;
            _imageProvider = imageProvider;

            _preview = new Property<Texture2D>();
            _drawingName = new Property<string>(Drawing.Name);
            _drawingDescription = new Property<string>(Drawing.Description);

            SelectDrawingCommand = new Command(SelectDrawing);
            _ = Update();
        }

        private async UniTask Update()
        {
            DrawingName = Drawing.Name;
            DrawingDescription = Drawing.Description;

            if (Drawing.Preview != null)
            {
                _preview.Value = Drawing.Preview.ToTexture2D(); 
                return;
            }
            
            try
            {
                _preview.Value = await _imageProvider.LoadImage(Drawing.Id.ToString());
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
            }
        }

        private void SelectDrawing()
        {
            _selectDrawing?.Invoke(Drawing);
        }
    }
}