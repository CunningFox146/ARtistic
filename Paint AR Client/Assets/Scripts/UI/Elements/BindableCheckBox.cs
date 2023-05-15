using ArPaint.UI.Elements;
using UnityEngine.UIElements;
using UnityMvvmToolkit.Common.Interfaces;
using UnityMvvmToolkit.Core;
using UnityMvvmToolkit.Core.Extensions;
using UnityMvvmToolkit.Core.Interfaces;

namespace ArPaint.UI.Elements
{
    public class BindableCheckBox : CheckBox, IBindableElement, IInitializable
    {
        private IReadOnlyProperty<string> _textProperty;
        private IProperty<bool> _isCheckedProperty;

        private PropertyBindingData _textPropertyBindingData;
        private PropertyBindingData _isCheckedPropertyBindingData;
        
        public string BindingTextPath { get; private set; }
        public string BindingIsCheckedPath { get; private set; }

        public void Initialize()
        {
            if (!string.IsNullOrEmpty(BindingTextPath))
                _textPropertyBindingData ??= BindingTextPath.ToPropertyBindingData();
            
            _isCheckedPropertyBindingData ??= BindingIsCheckedPath.ToPropertyBindingData();
        }

        public void SetBindingContext(IBindingContext context, IObjectProvider objectProvider)
        {
            if (_textPropertyBindingData != null)
            {
                _textProperty = objectProvider.RentReadOnlyProperty<string>(context, _textPropertyBindingData);
                _textProperty.ValueChanged += OnTextPropertyValueChanged;
                UpdateText(_textProperty.Value);
            }

            _isCheckedProperty = objectProvider.RentProperty<bool>(context, _isCheckedPropertyBindingData);
            _isCheckedProperty.ValueChanged += OnIsCheckedPropertyValueChanged;

            IsCheckedChanged += OnControlIsCheckedChanged;

            UpdateIsCheckedState(_isCheckedProperty.Value);
        }

        public void ResetBindingContext(IObjectProvider objectProvider)
        {
            if (_textProperty == null)
            {
                return;
            }

            _textProperty.ValueChanged -= OnTextPropertyValueChanged;
            _isCheckedProperty.ValueChanged -= OnIsCheckedPropertyValueChanged;

            objectProvider.ReturnReadOnlyProperty(_textProperty);
            objectProvider.ReturnProperty(_isCheckedProperty);

            _textProperty = null;
            _isCheckedProperty = null;

            IsCheckedChanged -= OnControlIsCheckedChanged;

            UpdateText(nameof(BindableCheckBox));
            UpdateIsCheckedState(false);
        }

        private void OnControlIsCheckedChanged(object sender, bool newValue)
        {
            _isCheckedProperty.Value = newValue;
        }

        private void OnTextPropertyValueChanged(object sender, string newText)
        {
            UpdateText(newText);
        }

        private void OnIsCheckedPropertyValueChanged(object sender, bool newValue)
        {
            UpdateIsCheckedState(newValue);
        }

        private void UpdateText(string text)
        {
            Text = text;
        }

        private void UpdateIsCheckedState(bool isChecked)
        {
            SetIsCheckedWithoutNotify(isChecked);
        }
        
        public new class UxmlFactory : UxmlFactory<BindableCheckBox, UxmlTraits>
        {
        }

        public new class UxmlTraits : CheckBox.UxmlTraits
        {
            private readonly UxmlStringAttributeDescription _bindingTextAttribute = new()
                { name = "binding-text-path", defaultValue = "" };

            private readonly UxmlStringAttributeDescription _bindingIsCheckedAttribute = new()
                { name = "binding-is-checked-path", defaultValue = "" };

            public override void Init(VisualElement visualElement, IUxmlAttributes bag, CreationContext context)
            {
                base.Init(visualElement, bag, context);

                var bindableCheckBox = (BindableCheckBox) visualElement;
                bindableCheckBox.BindingTextPath = _bindingTextAttribute.GetValueFromBag(bag, context);
                bindableCheckBox.BindingIsCheckedPath = _bindingIsCheckedAttribute.GetValueFromBag(bag, context);
            }
        }
    }
}