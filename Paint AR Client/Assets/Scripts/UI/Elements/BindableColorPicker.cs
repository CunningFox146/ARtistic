using UnityEngine;
using UnityEngine.UIElements;
using UnityMvvmToolkit.Core;
using UnityMvvmToolkit.Core.Extensions;
using UnityMvvmToolkit.Core.Interfaces;

namespace ArPaint.UI.Elements
{
    public class BindableColorPicker : ColorPicker, IBindableElement
    {
        private PropertyBindingData _colorPathBindingData;
        private IProperty<Color> _colorProperty;

        private string BindingColorPath { get; set; }


        public void SetBindingContext(IBindingContext context, IObjectProvider objectProvider)
        {
            _colorPathBindingData ??= BindingColorPath.ToPropertyBindingData();
            
            _colorProperty = objectProvider.RentProperty<Color>(context, _colorPathBindingData);
        }

        protected override void OnColorChanged()
        {
            base.OnColorChanged();
            
            if (_colorProperty != null)
                _colorProperty.Value = CurrentColor;
        }

        public void ResetBindingContext(IObjectProvider objectProvider)
        {
            if (_colorProperty == null)
                return;
            
            objectProvider.ReturnProperty(_colorProperty);
            _colorProperty = null;
        }

        public new class UxmlTraits : ColorPicker.UxmlTraits
        {
            private readonly UxmlStringAttributeDescription _bindingImageAttribute = new()
                { name = "binding-color-path", defaultValue = "" };

            public override void Init(VisualElement visualElement, IUxmlAttributes bag, CreationContext context)
            {
                base.Init(visualElement, bag, context);
                var view = (BindableColorPicker)visualElement;
                    
                view.BindingColorPath = _bindingImageAttribute.GetValueFromBag(bag, context);
            }
        }

        public new class UxmlFactory : UxmlFactory<BindableColorPicker, UxmlTraits>
        {
        }

    }
}