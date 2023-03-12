using System;
using System.Collections.Generic;
using ArPaint.Infrastructure.GameLoop;
using ArPaint.Services.Draw.Shapes;
using ArPaint.Services.Input;
using UnityEngine;
using Zenject;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace ArPaint.Services.Draw
{
    public class DrawService : IUpdateable, IDisposable
    {
        private readonly Dictionary<int, IShapeContainer> _activeShapes = new();
        private readonly IInputSource _inputSource;
        private readonly Camera _mainCamera;
        private readonly IFactory<IShapeContainer> _shapeContainerFactory;
        private readonly IUpdateLoop _updateLoop;

        public IShape Shape { get; set; } = new Rectangle();

        public DrawService(Camera mainCamera, IInputSource inputSource, ShapeContainer.Factory shapeContainerFactory,
            IUpdateLoop updateLoop)
        {
            _mainCamera = mainCamera;
            _inputSource = inputSource;
            _shapeContainerFactory = shapeContainerFactory;
            _updateLoop = updateLoop;

            _updateLoop.RegisterUpdate(this);
        }

        public void Dispose()
        {
            _updateLoop.UnregisterUpdate(this);
        }

        public void OnUpdate()
        {
            foreach (var touch in _inputSource.Touches)
            {
                if (touch.IsOverUI()) continue;

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
        }

        private void RegisterTouch(Touch touch)
        {
            var container = _shapeContainerFactory.Create();
            var touchPosition = touch.GetWorldPosition(_mainCamera, 1f);
            
            container.InitTransform(touchPosition, _mainCamera.transform.rotation);
            Shape.OnDrawStart(container, container.TransformPoint(touchPosition));
            
            _activeShapes.Add(touch.touchId, container);
        }

        private void OnTouchMove(Touch touch)
        {
            if (!_activeShapes.TryGetValue(touch.touchId, out var container)) return;
            
            var touchPosition = touch.GetWorldPosition(_mainCamera, 1f);
            Shape.OnDrawMove(container, container.TransformPoint(touchPosition));
        }

        private void UnregisterTouch(Touch touch)
        {
            if (!_activeShapes.Remove(touch.touchId, out var container)) return;
            
            var touchPosition = touch.GetWorldPosition(_mainCamera, 1f);
            Shape.OnDrawEnd(container, container.TransformPoint(touchPosition));
        }
    }
}