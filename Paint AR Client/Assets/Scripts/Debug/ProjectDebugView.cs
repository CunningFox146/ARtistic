using System;
using System.Collections.Generic;
using ArPaint.Infrastructure.GameStates;
using ArPaint.Services.Commands;
using ArPaint.Services.Draw;
using ArPaint.Services.Draw.Shapes;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using Zenject;

namespace ArPaint.Debug
{
    public class ProjectDebugView : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown _dropdown;
        private DiContainer _container;

        private void Awake()
        {
            _dropdown.options = new List<TMP_Dropdown.OptionData>
            {
                new(nameof(Line)),
                new(nameof(StraightLine)),
                new(nameof(Circle)),
                new(nameof(Oval)),
                new(nameof(Cube)),
                new(nameof(Rectangle))
            };
        }

        private void OnDestroy()
        {
            UnityEngine.Debug.Log("!!!!!!!!!!!!!!!destroyed", this);
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

        public void OnToolChanged(int idx)
        {
            _container.Resolve<IGameStateMachine>();
            var draw = _container.Resolve<DrawService>();
            draw.Shape = _dropdown.options[idx].text switch
            {
                nameof(Line) => new Line(),
                nameof(StraightLine) => new StraightLine(),
                nameof(Circle) => new Circle(),
                nameof(Oval) => new Oval(),
                nameof(Cube) => new Cube(),
                nameof(Rectangle) => new Rectangle(),
                _ => new Line()
            };
        }
    }
}