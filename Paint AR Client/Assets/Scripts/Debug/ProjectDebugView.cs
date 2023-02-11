using ArPaint.Services.Commands;
using UnityEngine;
using Zenject;

namespace ArPaint.Debug
{
    public class ProjectDebugView : MonoBehaviour
    {
        
        private DiContainer _container;

        private void Awake()
        {
#if UNITY_EDITOR
            DontDestroyOnLoad(gameObject);
#else
            Destroy(gameObject);
#endif
        }

        [Inject]
        private void Constructor(DiContainer container)
        {
            _container = container;
        }

        public void UndoLastCommand()
        {
            var commands = _container.Resolve<ICommandBuffer>();
            commands?.UndoLastCommand();
        }
    }
}