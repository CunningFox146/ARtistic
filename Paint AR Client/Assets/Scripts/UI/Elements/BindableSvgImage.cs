using UnityEngine.UIElements;
using UnityMvvmToolkit.Core;
using UnityMvvmToolkit.Core.Extensions;
using UnityMvvmToolkit.Core.Interfaces;

namespace ArPaint.UI.Elements
{
    public class BindableSvgImage : Image, IBindableElement
    {
        private PropertyBindingData _imageBindingData;
        private IProperty<VectorImage> _imageProperty;
        
        private string ImageBindingPath { get; set; }
        
        public void SetBindingContext(IBindingContext context, IObjectProvider objectProvider)
        {
            _imageBindingData ??= ImageBindingPath.ToPropertyBindingData();
            _imageProperty = objectProvider.RentProperty<VectorImage>(context, _imageBindingData);
            _imageProperty.ValueChanged += OnImageChanged;

            SetImage(_imageProperty.Value);
        }

        public void ResetBindingContext(IObjectProvider objectProvider)
        {
            if (_imageProperty == null)
                return;

            _imageProperty.ValueChanged -= OnImageChanged;
            objectProvider.ReturnProperty(_imageProperty);
            _imageProperty = null;
        }

        private void OnImageChanged(object sender, VectorImage image)
        {
            SetImage(image);
        }

        public new class UxmlTraits : Image.UxmlTraits
        {
            private readonly UxmlStringAttributeDescription _imageBindingPath = new()
                { name = "image-binding-path", defaultValue = string.Empty };

            public override void Init(VisualElement visualElement, IUxmlAttributes bag, CreationContext context)
            {
                base.Init(visualElement, bag, context);
                var view = (BindableSvgImage)visualElement;
                    
                view.ImageBindingPath = _imageBindingPath.GetValueFromBag(bag, context);
            }
        }

        public new class UxmlFactory : UxmlFactory<BindableSvgImage, UxmlTraits>
        {
        }
    }
}