using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ArPaint.Infrastructure.GameLoop;
using ArPaint.Services.Commands;
using ArPaint.Services.Draw.Brushes;
using ArPaint.Services.Draw.Shapes;
using ArPaint.Services.Input;
using ArPaint.Services.SaveLoad;
using ArPaint.Utils;
using Cysharp.Threading.Tasks;
using Firebase.Analytics;
using Services.ImageProvider;
using Services.PreviewRenderer;
using Services.StaticData;
using UnityEngine;
using Zenject;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace ArPaint.Services.Draw
{
    public class DrawService : IUpdateable, IDisposable, IDrawService, IDrawingContainer
    {
        private readonly Dictionary<int, IShapeContainer> _activeShapes = new();
        private readonly IInputSource _inputSource;
        private readonly Camera _mainCamera;
        private readonly IFactory<IShapeContainer> _shapeContainerFactory;
        private readonly IUpdateLoop _updateLoop;
        private readonly ICommandBuffer _commandBuffer;
        private readonly IDrawingsProvider _drawingsProvider;
        private readonly IPreviewRenderer _previewRenderer;
        private readonly IImageProvider _imageProvider;

        private IShape _shape;


        public IShape Shape
        {
            get => _shape;
            set
            {
                _shape = value;
                FirebaseAnalytics.LogEvent(AnalyticsEvents.ShapeSelect,
                    new Parameter("shape", _shape.GetType().Name));
            }
        }

        public bool IsActive { get; set; }
        public Transform Container { get; }
        public Brush Brush { get; set; } 

        public DrawService(Camera mainCamera, IInputSource inputSource, ShapeContainer.Factory shapeContainerFactory,
            IUpdateLoop updateLoop, ICommandBuffer commandBuffer, IStaticDataService staticData, IDrawingsProvider drawingsProvider, IPreviewRenderer previewRenderer, IImageProvider imageProvider)
        {
            Container = new GameObject { name = "Shapes Container"}.transform;
            Container.gameObject.SetActive(false);
            
            _mainCamera = mainCamera;
            _inputSource = inputSource;
            _shapeContainerFactory = shapeContainerFactory;
            _updateLoop = updateLoop;
            _commandBuffer = commandBuffer;
            _drawingsProvider = drawingsProvider;
            _previewRenderer = previewRenderer;
            _imageProvider = imageProvider;

            Shape = staticData.Shapes.ShapesList.FirstOrDefault();
            Brush = Brush.Default;

            _updateLoop.RegisterUpdate(this);

            if (_drawingsProvider.SelectedDrawing != null)
            {
                Load(_drawingsProvider.SelectedDrawing.DrawCommands);
            }
        }

        private void Load(IReadOnlyCollection<SerializableDrawCommand> serializableDrawCommands)
        {
            if (serializableDrawCommands == null)
                return;

            var copy = serializableDrawCommands.ToList();
            copy.Reverse();
            foreach (var command in copy)
            {
                var drawCommand = (DrawCommand)command;
                drawCommand.CreateContainer = CreateShapeContainer;
                _commandBuffer.AddCommand(drawCommand);
            }
        }

        public async UniTask Save()
        {
            var drawing = _drawingsProvider.SelectedDrawing;
            if (drawing != null)
            {
                var commands = _commandBuffer.SerializeDrawCommands();
                drawing.DrawCommands = commands; 
                
                await _previewRenderer.RenderDrawing(commands);
                drawing.Preview = await _previewRenderer.RenderTexture.ToBytesArray(128, 128);
                
                
                if (drawing.IsPublished)
                    await _imageProvider.UploadImage(drawing.Id.ToString(), drawing.Preview);
                await _drawingsProvider.Save();
            }
        }

        public void OnUpdate()
        {
            if (!IsActive)
                return;
            
            foreach (var touch in _inputSource.Touches)
            {
                if (touch.IsOverUI() && !_activeShapes.ContainsKey(touch.touchId))
                    continue;

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        RegisterTouch(touch);
                        break;
                    case TouchPhase.Ended:
                    case TouchPhase.Canceled:
                        UnregisterTouch(touch);
                        break;
                    case TouchPhase.Moved:
                        OnTouchMove(touch);
                        break;
                }
            }

            foreach (var shape in _activeShapes.Values)
                shape.SetRotation(_mainCamera.transform.rotation);
        }

        private IShapeContainer CreateShapeContainer()
        {
            var container = _shapeContainerFactory.Create();
            container.SetParent(Container);
            return container;
        }

        private void RegisterTouch(Touch touch)
        {
            if (!IsTouchValid(touch))
                return;

            var container = CreateShapeContainer();
            var touchPosition = touch.GetWorldPosition(_mainCamera, 1f);

            container.SetBrush(Brush);
            container.InitTransform(touchPosition, _mainCamera.transform.rotation);

            (Shape as IShapeStart)?.OnDrawStart(container, container.TransformPoint(touchPosition));

            _activeShapes.Add(touch.touchId, container);
        }

        private void OnTouchMove(Touch touch)
        {
            if (!touch.valid || !_activeShapes.TryGetValue(touch.touchId, out var container))
                return;

            var touchPosition = touch.GetWorldPosition(_mainCamera, 1f);
            Shape.OnDrawMove(container, container.TransformPoint(touchPosition));
        }

        private void UnregisterTouch(Touch touch)
        {
            if (!touch.valid || !_activeShapes.Remove(touch.touchId, out var container))
                return;

            var touchPosition = touch.GetWorldPosition(_mainCamera, 1f);
            (Shape as IShapeEnd)?.OnDrawEnd(container, container.TransformPoint(touchPosition));

            var command = new DrawCommand
            {
                Brush = Brush,
                ShapeContainer = container,
                ShapeData = (container as ISavable<ShapeData>)?.GetData(),
                CreateContainer = CreateShapeContainer
            };
            _commandBuffer.AddCommand(command, true);
        }

        private bool IsTouchValid(Touch touch)
        {
            return touch.valid && !_activeShapes.ContainsKey(touch.touchId);
        }
        
        public void Dispose()
        {
            _updateLoop.UnregisterUpdate(this);
        }
    }
}