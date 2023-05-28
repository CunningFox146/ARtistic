using UnityEngine;
using UnityEngine.UIElements;
using UnityMvvmToolkit.Core;
using UnityMvvmToolkit.Core.Extensions;
using UnityMvvmToolkit.Core.Interfaces;

namespace ArPaint.UI.Elements
{
    public class BindablePreviewController : VisualElement, IBindableElement
    {
        private PropertyBindingData _previewRotationPathData;
        private IProperty<Quaternion> _previewRotation;
        
        private float Sensitivity { get; set; }
        private string BindingPreviewRotationPath { get; set; }
        private Quaternion Rotation
        {
            get => _previewRotation.Value;
            set => _previewRotation.Value = value;
        }

        public BindablePreviewController()
        {
            RegisterCallback<MouseMoveEvent>(OnMouseMove);
        }

        private void OnMouseMove(MouseMoveEvent evt)
        {
            var delta = -evt.mouseDelta.x;
            Rotation = Mathf.Approximately(Rotation.eulerAngles.magnitude, 0f) ?
                Quaternion.Euler(delta * Sensitivity * Vector3.up) :
                Quaternion.Euler(delta * Sensitivity * Vector3.up) * Rotation;
        }

        public void SetBindingContext(IBindingContext context, IObjectProvider objectProvider)
        {
            _previewRotationPathData ??= BindingPreviewRotationPath.ToPropertyBindingData();
            
            _previewRotation = objectProvider.RentProperty<Quaternion>(context, _previewRotationPathData);
        }

        public void ResetBindingContext(IObjectProvider objectProvider)
        {
            if (_previewRotation == null)
                return;

            objectProvider.ReturnProperty(_previewRotation);
            _previewRotation = null;
        }
        
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            
            private readonly UxmlFloatAttributeDescription _sensitivityAttribute = new()
                { name = "sensitivity-path", defaultValue = 0.25f };
            
            private readonly UxmlStringAttributeDescription _bindingPreviewRotationAttribute = new()
                { name = "binding-preview-rotation-path", defaultValue = string.Empty };

            public override void Init(VisualElement visualElement, IUxmlAttributes bag, CreationContext context)
            {
                base.Init(visualElement, bag, context);
                var view = (BindablePreviewController)visualElement;
                    
                view.Sensitivity = _sensitivityAttribute.GetValueFromBag(bag, context);
                view.BindingPreviewRotationPath = _bindingPreviewRotationAttribute.GetValueFromBag(bag, context);
            }
        }
        
        public new class UxmlFactory : UxmlFactory<BindablePreviewController, UxmlTraits>
        {
        }
    }
}