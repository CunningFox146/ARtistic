using UnityEngine.UIElements;
using UnityMvvmToolkit.Core;
using UnityMvvmToolkit.Core.Extensions;
using UnityMvvmToolkit.Core.Interfaces;

namespace ArPaint.UI.Elements
{
    public class BindableSlider : FillableSlider, IBindableElement
    {
        private PropertyBindingData _valueBindingData;
        private IProperty<float> _valueProperty;
        
        public string ValueBindingPath { get; private set; }
        
        public void SetBindingContext(IBindingContext context, IObjectProvider objectProvider)
        {
            _valueBindingData ??= ValueBindingPath.ToPropertyBindingData();

            _valueProperty = objectProvider.RentProperty<float>(context, _valueBindingData);
            _valueProperty.ValueChanged += OnValueChanged;

            SetValueWithoutNotify(_valueProperty.Value);
            this.RegisterValueChangedCallback(OnValueChanged);
        }

        public void ResetBindingContext(IObjectProvider objectProvider)
        {
            if (_valueProperty == null)
                return;
            
            _valueProperty.ValueChanged -= OnValueChanged;
            objectProvider.ReturnProperty(_valueProperty);
            _valueProperty = null;
        }

        private void OnValueChanged(object sender, float val)
        {
            SetValueWithoutNotify(val);
        }

        private void OnValueChanged(ChangeEvent<float> evt)
        {
            _valueProperty.Value = evt.newValue;
        }

        public new class UxmlTraits : FillableSlider.UxmlTraits
        {
            private readonly UxmlStringAttributeDescription _valueBindingPath = new()
                { name = "value-binding-path", defaultValue = string.Empty };

            public override void Init(VisualElement visualElement, IUxmlAttributes bag, CreationContext context)
            {
                base.Init(visualElement, bag, context);
                var view = (BindableSlider)visualElement;
                    
                view.ValueBindingPath = _valueBindingPath.GetValueFromBag(bag, context);
            }
        }
        
        public new class UxmlFactory : UxmlFactory<BindableSlider, UxmlTraits>
        {
        }
    }
}