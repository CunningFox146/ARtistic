using System.Collections.Generic;
using ArPaint.Services.Commands;
using ArPaint.Services.Draw;
using ArPaint.Services.Draw.Shapes;
using Services.StaticData;
using TMPro;
using UnityEngine;
using Zenject;

namespace ArPaint.Debug
{
    public class ProjectDebugView : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown _dropdown;
        private DiContainer _container;
        private List<Shape> _shapes;

        private void Awake()
        {
            var data = _container.Resolve<IStaticDataService>();
            _dropdown.options = new List<TMP_Dropdown.OptionData>();
            _shapes = data.Shapes.ShapesList;
            _shapes.ForEach(shape => _dropdown.options.Add(new(shape.Name)));
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
        
        public void RedoLastCommand()
        {
            var commands = _container.Resolve<ICommandBuffer>();
            commands?.RedoLastCommand();
        }

        public void OnToolChanged(int idx)
        {
            var draw = _container.Resolve<DrawService>();
            draw.Shape = _shapes[idx];
        }
    }
}