using System;
using ArPaint.Infrastructure.GameLoop;
using ArPaint.Services.Input;
using UnityEngine;
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

        public DrawService(Camera mainCamera, IInputSource inputSource, ILineSource lineSource, IUpdateLoop updateLoop)
        {
            _mainCamera = mainCamera;
            _inputSource = inputSource;
            _lineSource = lineSource;
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
            line.transform.position = touch.GetWorldPosition(_mainCamera);

            // var a = new GameObject();
            // ARAnchor anchor = _anchorManager.AddAnchor(new Pose(touch.GetWorldPosition(_mainCamera), Quaternion.identity));
            // if (anchor == null) 
            //     Debug.LogError("Error creating reference point");
            // else 
            // {
            //     anchors.Add(anchor);
            //     ARDebugManager.Instance.LogInfo($"Anchor created & total of {anchors.Count} anchor(s)");
            // }
        }

        private void OnTouchMove(Touch touch)
        {
            // throw new NotImplementedException();
        }

        private void UnregisterTouch(Touch touch)
        {
            // throw new NotImplementedException();
        }
    }
}