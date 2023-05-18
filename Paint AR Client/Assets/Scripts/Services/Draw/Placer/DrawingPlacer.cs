using System;
using System.Collections.Generic;
using System.Linq;
using ArPaint.Infrastructure.GameLoop;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace ArPaint.Services.Draw.Placer
{
    public class DrawingPlacer : IDisposable, IUpdateable, IDrawingPlacer
    {
        public event Action<bool> PlacementValidChanged;
        
        private readonly IDrawingContainer _container;
        private readonly ARRaycastManager _raycastManager;
        private readonly Camera _mainCamera;
        private readonly IUpdateLoop _updateLoop;
        private readonly List<ARRaycastHit> _raycastHits;

        private bool _isPlacementValid;
        public bool IsPlacementValid
        {
            get => _isPlacementValid;
            private set
            {
                if (_isPlacementValid == value)
                    return;
                _isPlacementValid = value;
                PlacementValidChanged?.Invoke(_isPlacementValid);
            }
        }

        public DrawingPlacer(IDrawingContainer container, ARRaycastManager raycastManager, Camera mainCamera, IUpdateLoop updateLoop)
        {
            _container = container;
            _raycastManager = raycastManager;
            _mainCamera = mainCamera;
            _updateLoop = updateLoop;

            _raycastHits = new();
        }


        public void StartPlacing()
        {
            _updateLoop.RegisterUpdate(this);
        }

        public void StopPlacing(bool disableContainer = false)
        {
            _updateLoop.UnregisterUpdate(this);
            if (_container != null && _container?.Container)
                _container?.Container.gameObject.SetActive(!disableContainer);
        }

        public void Dispose()
        {
            _updateLoop.UnregisterUpdate(this);
        }

        public void OnUpdate()
        {
            _raycastHits.Clear();
            var screenCenter = _mainCamera.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
            if (!_raycastManager.Raycast(screenCenter, _raycastHits, TrackableType.Planes) || !_raycastHits.Any())
            {
                IsPlacementValid = false;
                _container.Container.gameObject.SetActive(false);
                return;
            }
            
            var pose = _raycastHits[0].pose;
            var cameraForward = _mainCamera.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0f, cameraForward.z).normalized;
            pose.rotation = Quaternion.LookRotation(cameraBearing);
            
            _container.Container.gameObject.SetActive(true);
            _container.Container.SetPositionAndRotation(pose.position, pose.rotation);

            IsPlacementValid = true;
        }
    }
}