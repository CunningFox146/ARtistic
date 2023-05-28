﻿using System;
using System.Collections.Generic;
using ArPaint.Infrastructure.GameLoop;
using ArPaint.Services.Commands;
using ArPaint.Services.Draw.Shapes;
using ArPaint.Utils;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using Zenject;
using Object = UnityEngine.Object;

namespace Services.PreviewRenderer
{
    public class PreviewRenderer : IPreviewRenderer, IUpdateable, IDisposable
    {
        private readonly Camera _camera;
        private readonly Transform _itemPreviewContainer; // Transform that acts as the center of object
        private readonly Transform _itemRotationContainer; // Transform to rotate
        private readonly IFactory<IShapeContainer> _shapeContainerFactory;
        private readonly IUpdateLoop _updateLoop;
        private Transform _container;
        private Func<Quaternion> _targetRotation;

        public RenderTexture RenderTexture => _camera.targetTexture;

        public PreviewRenderer(ShapeContainer.Factory shapeContainerFactory, IUpdateLoop updateLoop, RenderTexture renderTexture)
        {
            _itemRotationContainer = new GameObject { name = "RotationContainer" }.transform;
            _itemPreviewContainer = new GameObject { name = "PreviewContainer" }.transform;

            _itemPreviewContainer.SetParent(_itemRotationContainer);

            var cameraObject = new GameObject("PreviewRendererCamera", typeof(Camera));
            _camera = cameraObject.GetComponent<Camera>();
            _camera.targetTexture = renderTexture;
            _camera.backgroundColor = new Color(0f, 0f, 0f, 0f);
            _camera.clearFlags = CameraClearFlags.Color;
            _camera.cullingMask = 1 << (int)Layers.Preview;
            _camera.enabled = false;
            
            _shapeContainerFactory = shapeContainerFactory;
            _updateLoop = updateLoop;
        }

        public void Dispose()
        {
            _updateLoop.UnregisterUpdate(this);
        }

        public void SetRotationGetter(Func<Quaternion> rotation)
        {
            _targetRotation = rotation;
        }

        public async UniTask RenderDrawing(IEnumerable<SerializableDrawCommand> commands)
        {
            ClearInternal(false);

            _camera.enabled = true;

            foreach (var command in commands)
            {
                var drawCommand = (DrawCommand)command;
                drawCommand.CreateContainer = CreateShapeContainer;
                drawCommand.Perform();
            }
            
            _itemRotationContainer.SetLayerRecursively((int)Layers.Preview);

            await UniTask.Yield();

            var bounds = GetBoundsWithChildren(_container.gameObject);
            _itemPreviewContainer.position = -bounds.center;

            FocusOn(1.1f, _camera);
            
            _updateLoop.RegisterUpdate(this);
            
            await UniTask.Yield();
        }

        public void Clear()
        {
            ClearInternal(true);
        }

        private void ClearInternal(bool disableCamera)
        {
            _camera.enabled = !disableCamera;

            if (_container != null)
                Object.Destroy(_container.gameObject);

            
            _itemRotationContainer.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
            _itemPreviewContainer.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
            
            _container = new GameObject { name = "ShapesContainer" }.transform;
            _container.SetParent(_itemPreviewContainer);

            _updateLoop.UnregisterUpdate(this);
        }

        public void OnUpdate()
        {
            if (!_itemRotationContainer || _targetRotation == null)
                return;

            _itemRotationContainer.rotation =
                Quaternion.Slerp(_itemRotationContainer.rotation, _targetRotation(), 0.5f);
        }

        private IShapeContainer CreateShapeContainer()
        {
            var container = _shapeContainerFactory.Create();
            container.SetParent(_container);
            return container;
        }

        private void FocusOn(float marginPercentage, Camera camera)
        {
            var bounds = GetBoundsWithChildren(_itemRotationContainer.gameObject);

            var center = bounds.center;
            var length = (bounds.max - bounds.min).magnitude;
            camera.transform.position = new Vector3(0f, center.y, length * marginPercentage);
            camera.transform.LookAt(center);
            camera.gameObject.SetActive(true);
        }

        private static Bounds GetBoundsWithChildren(GameObject gameObject)
        {
            var renderers = gameObject.GetComponentsInChildren<Renderer>(false);
            var bounds = renderers.Length > 0 ? renderers[0].bounds : new Bounds();

            for (var i = 1; i < renderers.Length; i++)
            {
                if (!renderers[i].enabled) continue;
                bounds.Encapsulate(renderers[i].bounds);
            }

            return bounds;
        }
    }
}