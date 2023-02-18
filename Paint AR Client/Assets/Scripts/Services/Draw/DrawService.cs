using System;
using System.Collections.Generic;
using ArPaint.Infrastructure.GameLoop;
using ArPaint.Services.Draw.Drawables;
using ArPaint.Services.Input;
using UnityEngine;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace ArPaint.Services.Draw
{
    public class DrawService : IUpdateable, IDisposable
    {
        private readonly Dictionary<int, IDrawable> _activeLines;
        private readonly IDrawableSource _drawableSource;
        private readonly IInputSource _inputSource;
        private readonly Camera _mainCamera;
        private readonly IUpdateLoop _updateLoop;

        public DrawService(Camera mainCamera, IInputSource inputSource, IDrawableSource drawableSource,
            IUpdateLoop updateLoop)
        {
            _mainCamera = mainCamera;
            _inputSource = inputSource;
            _drawableSource = drawableSource;
            _updateLoop = updateLoop;

            _activeLines = new Dictionary<int, IDrawable>();

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
            var line = _drawableSource.Get(typeof(DrawBase));
            var worldPos = touch.GetWorldPosition(_mainCamera, 1f);
            line.OnDrawStart(worldPos);
            _activeLines.Add(touch.touchId, line);
        }

        private void OnTouchMove(Touch touch)
        {
            if (!_activeLines.TryGetValue(touch.touchId, out var line)) return;
            line.OnDrawMove(touch.GetWorldPosition(_mainCamera, 1f));
        }

        private void UnregisterTouch(Touch touch)
        {
            if (!_activeLines.Remove(touch.touchId, out var line)) return;
            line.OnDrawEnd(touch.GetWorldPosition(_mainCamera, 1f));
        }
    }
}