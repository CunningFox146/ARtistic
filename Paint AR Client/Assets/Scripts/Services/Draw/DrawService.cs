using System;
using System.Collections.Generic;
using ArPaint.Infrastructure.GameLoop;
using ArPaint.Services.Input;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using Object = UnityEngine.Object;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace ArPaint.Services.Draw
{
    public class DrawService : IUpdateable, IDisposable
    {
        private readonly IInputSource _inputSource;
        private readonly ILineSource _lineSource;
        private readonly Camera _mainCamera;
        private readonly IUpdateLoop _updateLoop;
        private readonly Dictionary<int, Line> _activeLines;

        public DrawService(Camera mainCamera, IInputSource inputSource, ILineSource lineSource, IUpdateLoop updateLoop)
        {
            _mainCamera = mainCamera;
            _inputSource = inputSource;
            _lineSource = lineSource;
            _updateLoop = updateLoop;

            _activeLines = new();

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
            var line = _lineSource.Get();
            var worldPos = touch.GetWorldPosition(_mainCamera, 1f);
            line.SetPosition(worldPos);
            _activeLines.Add(touch.touchId, line);
        }

        private void OnTouchMove(Touch touch)
        {
            if (!_activeLines.TryGetValue(touch.touchId, out var line)) return;
            line.AppendPosition(touch.GetWorldPosition(_mainCamera, 1f));
        }

        private void UnregisterTouch(Touch touch)
        {
            _activeLines.Remove(touch.touchId);
        }
    }
}