using System;
using UnityEngine.UIElements;

namespace ArPaint.UI.Elements
{
    public class Switch : VisualElement
    {
        public event EventHandler<bool> IsCheckedChanged;

        private const string ElementClassName = "switch";
        private static readonly string enabledClassName = $"{ElementClassName}--on";
        private static readonly string circleClassName = $"{ElementClassName}__circle";

        private readonly VisualElement _circle;
        private bool _isChecked;

        public bool IsChecked
        {
            get => _isChecked;
            set => SetIsChecked(value, true);
        }
        
        public void SetIsCheckedWithoutNotify(bool isChecked)
        {
            SetIsChecked(isChecked, false);
        }

        private void SetIsChecked(bool value, bool notify)
        {
            _isChecked = value;
            UpdateVisuals();
            
            if (notify)
            {
                IsCheckedChanged?.Invoke(this, value);
            }
        }

        private void UpdateVisuals()
        {
            if (_isChecked)
                AddToClassList(enabledClassName);
            else
                RemoveFromClassList(enabledClassName);
        }

        public Switch()
        {
            AddToClassList(ElementClassName);
            
            _circle = new VisualElement();
            _circle.name = "circle";
            _circle.AddToClassList(circleClassName);
            _circle.pickingMode = PickingMode.Ignore;
            Add(_circle);
            
            RegisterCallback<ClickEvent>(OnToggleClick);
        }

        private void OnToggleClick(ClickEvent evt)
        {
            evt.StopImmediatePropagation();
            IsChecked = !IsChecked;
        }

        public new class UxmlFactory : UxmlFactory<Switch, UxmlTraits>
        {
        }

        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            private readonly UxmlBoolAttributeDescription _textAttribute = new()
                { name = "is-on", defaultValue = false };

            public override void Init(VisualElement visualElement, IUxmlAttributes bag, CreationContext context)
            {
                base.Init(visualElement, bag, context);
                ((Switch) visualElement).IsChecked = _textAttribute.GetValueFromBag(bag, context);
            }
        }
    }
}