using System;
using System.Collections.Generic;
using ArPaint.Infrastructure.GameLoop;
using ArPaint.Services.Commands;
using ArPaint.Services.Draw.Shapes;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Services.PreviewRenderer
{
    public class PreviewRenderer : IPreviewRenderer, IUpdateable, IDisposable
    {
        private readonly IFactory<IShapeContainer> _shapeContainerFactory;
        private Transform _container;
        private readonly Transform _itemRotationContainer; // Transform to rotate
        private readonly Transform _itemPreviewContainer; // Transform that acts as the center of object
        private readonly Camera _camera;
        private readonly IUpdateLoop _updateLoop;

        public PreviewRenderer(ShapeContainer.Factory shapeContainerFactory, Camera camera, IUpdateLoop updateLoop)
        {
            _itemRotationContainer = new GameObject { name = "RotationContainer"}.transform;
            _itemPreviewContainer = new GameObject { name = "PreviewContainer"}.transform;
            
            _itemPreviewContainer.SetParent(_itemRotationContainer);
            
            _shapeContainerFactory = shapeContainerFactory;
            _camera = camera;
            _updateLoop = updateLoop;
        }

        public async void RenderDrawing(IEnumerable<SerializableDrawCommand> commands)
        {
            Clear();
            
            _updateLoop.RegisterUpdate(this);
            
            _itemRotationContainer.localRotation = Quaternion.identity;
            _itemPreviewContainer.position = Vector3.zero;
            
            foreach (var command in commands)
            {
                var drawCommand = (DrawCommand)command;
                drawCommand.CreateContainer = CreateShapeContainer;
                drawCommand.Perform();
            }

            await UniTask.Yield();
            
            var bounds = GetBoundsWithChildren(_container.gameObject);
            _itemPreviewContainer.position = -bounds.center;

            FocusOn(1.1f, _camera);
        }

        public void Clear()
        {
            if (_container !=null)
                Object.Destroy(_container.gameObject);
            
            _container = new GameObject { name = "ShapesContainer"}.transform;
            _container.SetParent(_itemPreviewContainer);
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
        
        private static void FocusOn_OLD(Bounds bounds, float marginPercentage, Camera camera)
        {
            var centerAtFront = new Vector3(bounds.center.x, bounds.center.y, bounds.max.z);
            var centerAtFrontTop = new Vector3(bounds.center.x, bounds.max.y, bounds.max.z);
            var centerToTopDist = (centerAtFrontTop - centerAtFront).magnitude;
            var minDistance = centerToTopDist * marginPercentage / Mathf.Tan(camera.fieldOfView * Mathf.Deg2Rad);
 
            camera.transform.position = new Vector3(bounds.center.x, bounds.center.y, -minDistance);
            camera.transform.LookAt(bounds.center);
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

        public void OnUpdate()
        {
            _itemRotationContainer.Rotate(Time.deltaTime * 10f * Vector3.up);
        }

        public void Dispose()
        {
            _updateLoop.UnregisterUpdate(this);
        }
    }
}