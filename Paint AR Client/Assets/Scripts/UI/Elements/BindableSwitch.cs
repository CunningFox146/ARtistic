using UnityEngine.UIElements;
using UnityMvvmToolkit.Core;
using UnityMvvmToolkit.Core.Extensions;
using UnityMvvmToolkit.Core.Interfaces;

namespace ArPaint.UI.Elements
{
    public class BindableSwitch : Switch, IBindableElement
    {
        private PropertyBindingData _valueBindingData;
        private IProperty<bool> _valueProperty;
        
        private string BindingPath { get; set; }
        
        public void SetBindingContext(IBindingContext context, IObjectProvider objectProvider)
        {
            _valueBindingData ??= BindingPath.ToPropertyBindingData();
            _valueProperty = objectProvider.RentProperty<bool>(context, _valueBindingData);
            _valueProperty.ValueChanged += OnValueChanged;
            
            IsCheckedChanged += OnIsCheckedChanged;

            SetIsCheckedWithoutNotify(_valueProperty.Value);
        }

        public void ResetBindingContext(IObjectProvider objectProvider)
        {
            if (_valueProperty == null)
                return;

            IsCheckedChanged -= OnIsCheckedChanged;
            _valueProperty.ValueChanged -= OnValueChanged;
            objectProvider.ReturnProperty(_valueProperty);
            _valueProperty = null;
        }

        private void OnValueChanged(object sender, bool value)
        {
            SetIsCheckedWithoutNotify(value);
        }

        private void OnIsCheckedChanged(object sender, bool val)
        {
            _valueProperty.Value = val;
        }

        public new class UxmlFactory : UxmlFactory<BindableSwitch, UxmlTraits>
        {
        }

        public new class UxmlTraits : Switch.UxmlTraits
        {
            private readonly UxmlStringAttributeDescription _valueBindingAttribute = new()
                { name = "value-binding-path", defaultValue = string.Empty };

            public override void Init(VisualElement visualElement, IUxmlAttributes bag, CreationContext context)
            {
                base.Init(visualElement, bag, context);
                ((BindableSwitch) visualElement).BindingPath = _valueBindingAttribute.GetValueFromBag(bag, context);
            }
        }
    }
}