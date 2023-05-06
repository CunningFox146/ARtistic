using ArPaint.Utils;
using UnityEngine.UIElements;
using UnityMvvmToolkit.Core;
using UnityMvvmToolkit.Core.Extensions;
using UnityMvvmToolkit.Core.Interfaces;

namespace ArPaint.UI.Elements
{
    public class BindableFocusView : VisualElement, IBindableElement
    {
        private PropertyBindingData _focusPathBindingData;
        private IProperty<bool> _focusProperty;

        public bool ReactOnFocusIn { get; set; }
        public bool ReactOnFocusOut { get; set; }
        public string BindingFocusPath { get; set; }

        public BindableFocusView()
        {
            RegisterCallback<FocusInEvent>(OnFocusIn);
            RegisterCallback<FocusOutEvent>(OnFocusOut);
        }

        private void OnFocusIn(FocusInEvent evt)
        {
            if (ReactOnFocusIn && _focusProperty != null)
                _focusProperty.Value = true;
        }

        private void OnFocusOut(FocusOutEvent evt)
        {
            if (ReactOnFocusOut && _focusProperty != null)
                _focusProperty.Value = false;
        }

        public void SetBindingContext(IBindingContext context, IObjectProvider objectProvider)
        {
            _focusPathBindingData ??= BindingFocusPath.ToPropertyBindingData();

            _focusProperty = objectProvider.RentProperty<bool>(context, _focusPathBindingData);
        }

        public void ResetBindingContext(IObjectProvider objectProvider)
        {
            if (_focusProperty == null)
                return;

            objectProvider.ReturnProperty(_focusProperty);
            _focusProperty = null;
        }

        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            private readonly UxmlBoolAttributeDescription _reactOnFocusInAttribute = new()
                { name = "react-on-focus-in", defaultValue = true };

            private readonly UxmlBoolAttributeDescription _reactOnFocusOutAttribute = new()
                { name = "react-on-focus-out", defaultValue = true };

            private readonly UxmlStringAttributeDescription _bindingImageAttribute = new()
                { name = "binding-focus-path", defaultValue = "" };

            public override void Init(VisualElement visualElement, IUxmlAttributes bag, CreationContext context)
            {
                base.Init(visualElement, bag, context);
                var view = (BindableFocusView)visualElement;
                    
                view.BindingFocusPath = _bindingImageAttribute.GetValueFromBag(bag, context);
                view.ReactOnFocusIn = _reactOnFocusInAttribute.GetValueFromBag(bag, context);
                view.ReactOnFocusOut = _reactOnFocusOutAttribute.GetValueFromBag(bag, context);
            }
        }

        public new class UxmlFactory : UxmlFactory<BindableFocusView, UxmlTraits>
        {
        }
    }
}