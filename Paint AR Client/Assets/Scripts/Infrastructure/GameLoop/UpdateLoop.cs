using System.Collections.Generic;
using UnityEngine;

namespace ArPaint.Infrastructure.GameLoop
{
    public class UpdateLoop : MonoBehaviour, IUpdateLoop, IFixedUpdateLoop, ILateUpdateLoop
    {
        private readonly List<IFixedUpdateable> _fixUpdateObjects = new();
        private readonly List<ILateUpdateable> _lateUpdateObjects = new();
        private readonly List<IUpdateable> _updateObjects = new();

        private void Update()
        {
            foreach (var updateable in _updateObjects) updateable.OnUpdate();
        }

        private void FixedUpdate()
        {
            foreach (var updateable in _fixUpdateObjects) updateable.OnFixedUpdate();
        }

        private void LateUpdate()
        {
            foreach (var updateable in _lateUpdateObjects) updateable.OnLateUpdate();
        }

        public void RegisterFixedUpdate(IFixedUpdateable updateable)
        {
            _fixUpdateObjects.Add(updateable);
        }

        public void UnregisterFixedUpdate(IFixedUpdateable updateable)
        {
            _fixUpdateObjects.Remove(updateable);
        }

        public void RegisterLateUpdate(ILateUpdateable updateable)
        {
            _lateUpdateObjects.Add(updateable);
        }

        public void UnregisterLateUpdate(ILateUpdateable updateable)
        {
            _lateUpdateObjects.Remove(updateable);
        }

        public void RegisterUpdate(IUpdateable updateable)
        {
            _updateObjects.Add(updateable);
        }

        public void UnregisterUpdate(IUpdateable updateable)
        {
            _updateObjects.Remove(updateable);
        }
    }
}