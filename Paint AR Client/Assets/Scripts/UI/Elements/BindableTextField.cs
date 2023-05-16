using UnityEngine.UIElements;
using UnityMvvmToolkit.Core;
using UnityMvvmToolkit.Core.Extensions;
using UnityMvvmToolkit.Core.Interfaces;

namespace ArPaint.UI.Elements
{
    public class BindableTextField : TextField, IBindableElement
    {
        private PropertyBindingData _valueBindingData;
        private IProperty<string> _valueProperty;
        
        private string TextBindingPath { get; set; }
        
        public void SetBindingContext(IBindingContext context, IObjectProvider objectProvider)
        {
            _valueBindingData ??= TextBindingPath.ToPropertyBindingData();
            _valueProperty = objectProvider.RentProperty<string>(context, _valueBindingData);
            _valueProperty.ValueChanged += OnValueChanged;

            this.RegisterValueChangedCallback(OnInputStringChanged);
            
            SetValueWithoutNotify(_valueProperty.Value);
        }

        private void OnInputStringChanged(ChangeEvent<string> evt)
        {
            _valueProperty.Value = evt.newValue;
        }

        private void OnValueChanged(object sender, string val)
        {
            SetValueWithoutNotify(val);
        }

        public void ResetBindingContext(IObjectProvider objectProvider)
        {
            if (_valueProperty == null)
                return;

            _valueProperty.ValueChanged -= OnValueChanged;
            objectProvider.ReturnProperty(_valueProperty);
            this.UnregisterValueChangedCallback(OnInputStringChanged);
            _valueProperty = null;
        }

        public new class UxmlFactory : UxmlFactory<BindableTextField, UxmlTraits>
        {
        }

        public new class UxmlTraits : TextField.UxmlTraits
        {
            private readonly UxmlStringAttributeDescription _bindingTextPathAttribute = new()
                { name = "text-binding-path", defaultValue = string.Empty };

            public override void Init(VisualElement visualElement, IUxmlAttributes bag, CreationContext context)
            {
                base.Init(visualElement, bag, context);
                ((BindableTextField) visualElement).TextBindingPath = _bindingTextPathAttribute.GetValueFromBag(bag, context);
            }
        }
    }
}