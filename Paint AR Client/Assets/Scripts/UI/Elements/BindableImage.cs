using UnityEngine;
using UnityEngine.UIElements;
using UnityMvvmToolkit.Core;
using UnityMvvmToolkit.Core.Extensions;
using UnityMvvmToolkit.Core.Interfaces;

namespace ArPaint.UI.Elements
{
    public class BindableImage : Image, IBindableElement
    {
        private PropertyBindingData _imagePathBindingData;
        private IProperty<Texture2D> _imageProperty;

        private string BindingImagePath { get; set; }

        public void SetBindingContext(IBindingContext context, IObjectProvider objectProvider)
        {
            _imagePathBindingData ??= BindingImagePath.ToPropertyBindingData();

            _imageProperty = objectProvider.RentProperty<Texture2D>(context, _imagePathBindingData);
            _imageProperty.ValueChanged += OnImagePropertyValueChanged;

            SetImage(_imageProperty.Value);
        }

        public void ResetBindingContext(IObjectProvider objectProvider)
        {
            if (_imageProperty == null)
            {
                return;
            }

            _imageProperty.ValueChanged -= OnImagePropertyValueChanged;

            objectProvider.ReturnProperty(_imageProperty);

            _imageProperty = null;

            SetImage(default(Texture2D));
        }

        private void OnImagePropertyValueChanged(object sender, Texture2D newImage)
        {
            SetImage(newImage);
        }

        public new class UxmlFactory : UxmlFactory<BindableImage, UxmlTraits> { }

        public new class UxmlTraits : Image.UxmlTraits
        {
            private readonly UxmlStringAttributeDescription _bindingImageAttribute = new()
                { name = "binding-image-path", defaultValue = "" };

            public override void Init(VisualElement visualElement, IUxmlAttributes bag, CreationContext context)
            {
                base.Init(visualElement, bag, context);
                ((BindableImage) visualElement).BindingImagePath = _bindingImageAttribute.GetValueFromBag(bag, context);
            }
        }
    }
}