using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ArPaint.Infrastructure.GameLoop;
using ArPaint.Services.Draw.Shapes;
using ArPaint.Services.Input;
using Cysharp.Threading.Tasks;
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

        public IShape Shape { get; set; } = new Circle();

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
            UnityEngine.Debug.Log("OnUpdate");
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

            foreach (var shape in _activeShapes.Values)
            {
                ((MonoBehaviour)shape).transform.rotation = _mainCamera.transform.rotation;
            }
        }

        private void RegisterTouch(Touch touch)
        {
            UnityEngine.Debug.Log("RegisterTouch");
            if (!IsTouchValid(touch)) return;
            
            var container = _shapeContainerFactory.Create();
            var touchPosition = touch.GetWorldPosition(_mainCamera, 1f);
            
            container.InitTransform(touchPosition, _mainCamera.transform.rotation);

            var mo = container as MonoBehaviour;
            UnityEngine.Debug.Log($"!!!!!!container {mo.transform.position} {mo.transform.eulerAngles}");
            // Shape.OnDrawStart(container, container.TransformPoint(touchPosition));
            
            _activeShapes.Add(touch.touchId, container);
        }

        private void OnTouchMove(Touch touch)
        {
            if (!touch.valid || !_activeShapes.TryGetValue(touch.touchId, out var container)) return;
            
            var touchPosition = touch.GetWorldPosition(_mainCamera, 1f);
            Shape.OnDrawMove(container, container.TransformPoint(touchPosition));
        }

        private void UnregisterTouch(Touch touch)
        {
            if (!touch.valid || !_activeShapes.Remove(touch.touchId, out var container)) return;
            
            var touchPosition = touch.GetWorldPosition(_mainCamera, 1f);
            Shape.OnDrawEnd(container, container.TransformPoint(touchPosition));
        }
        
        private bool IsTouchValid(Touch touch) => touch.valid && !_activeShapes.ContainsKey(touch.touchId);
    }
}