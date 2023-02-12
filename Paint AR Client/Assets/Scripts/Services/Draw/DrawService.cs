using System;
using System.Collections.Generic;
using ArPaint.Infrastructure.GameLoop;
using ArPaint.Services.Input;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace ArPaint.Services.Draw
{
    public class DrawService : IUpdateable, IDisposable
    {
        private readonly Camera _mainCamera;
        private readonly IInputSource _inputSource;
        private readonly IUpdateLoop _updateLoop;
        private readonly List<ARAnchor> _anchors;

        public DrawService(Camera mainCamera, IInputSource inputSource, IUpdateLoop updateLoop)
        {
            _mainCamera = mainCamera;
            _inputSource = inputSource;
            _updateLoop = updateLoop;
            _anchors = new();
            
            _updateLoop.RegisterUpdate(this);
        }

        public void OnUpdate()
        {
            foreach (var touch in _inputSource.Touches)
            {
                if (touch.IsOverUI()) continue;

                switch (touch.phase)
                {
                    case TouchPhase.Began: RegisterTouch(touch); break;
                    case TouchPhase.Canceled: UnregisterTouch(touch); break;
                    case TouchPhase.Moved: OnTouchMove(touch); break;
                    default: break;
                }
            }
        }

        private void RegisterTouch(Touch touch)
        {
            // var a = new GameObject();
            // var anch = a.AddComponent<ARAnchor>();
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
            throw new NotImplementedException();
        }

        private void UnregisterTouch(Touch touch)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _updateLoop.UnregisterUpdate(this);
        }
    }
}