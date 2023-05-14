using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace ArPaint.UI.Elements
{
    public class AspectRatioPanel : VisualElement
    {
        public int AspectRatioX { get; set; } = 16;
        public int AspectRatioY { get; set; } = 9;
        public int BalanceX { get; set; } = 50;
        public int BalanceY { get; set; } = 50;


        public AspectRatioPanel()
        {
            style.position = Position.Absolute;
            style.left = 0;
            style.top = 0;
            style.right = StyleKeyword.Undefined;
            style.bottom = StyleKeyword.Undefined;
            RegisterCallback<AttachToPanelEvent>(OnAttachToPanelEvent);
        }


        private void OnAttachToPanelEvent(AttachToPanelEvent e)
        {
            parent?.RegisterCallback<GeometryChangedEvent>(OnGeometryChangedEvent);
            FitToParent();
        }


        private void OnGeometryChangedEvent(GeometryChangedEvent e)
        {
            FitToParent();
        }


        private void FitToParent()
        {
            if (parent == null)
                return;
            
            var parentW = parent.resolvedStyle.width;
            var parentH = parent.resolvedStyle.height;
            
            if (float.IsNaN(parentW) || float.IsNaN(parentH))
                return;

            style.position = Position.Absolute;
            style.left = 0;
            style.top = 0;
            style.right = StyleKeyword.Undefined;
            style.bottom = StyleKeyword.Undefined;

            if (AspectRatioX <= 0.0f || AspectRatioY <= 0.0f)
            {
                style.width = parentW;
                style.height = parentH;
                return;
            }

            var ratio = Mathf.Min(parentW / AspectRatioX, parentH / AspectRatioY);
            var targetW = Mathf.Floor(AspectRatioX * ratio);
            var targetH = Mathf.Floor(AspectRatioY * ratio);
            style.width = targetW;
            style.height = targetH;

            var marginX = parentW - targetW;
            var marginY = parentH - targetH;
            style.left = Mathf.Floor(marginX * BalanceX / 100.0f);
            style.top = Mathf.Floor(marginY * BalanceY / 100.0f);
        }

        public new class UxmlFactory : UxmlFactory<AspectRatioPanel, UxmlTraits>
        {
        }

        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            private readonly UxmlIntAttributeDescription _aspectRatioX = new()
                { name = "aspect-ratio-x", defaultValue = 16, restriction = new UxmlValueBounds { min = "1" } };

            private readonly UxmlIntAttributeDescription _aspectRatioY = new()
                { name = "aspect-ratio-y", defaultValue = 9, restriction = new UxmlValueBounds { min = "1" } };

            private readonly UxmlIntAttributeDescription _balanceX = new()
                { name = "balance-x", defaultValue = 50, restriction = new UxmlValueBounds { min = "0", max = "100" } };

            private readonly UxmlIntAttributeDescription _balanceY = new()
                { name = "balance-y", defaultValue = 50, restriction = new UxmlValueBounds { min = "0", max = "100" } };


            public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
            {
                get { yield break; }
            }

            public override void Init(VisualElement visualElement, IUxmlAttributes attributes,
                CreationContext creationContext)
            {
                base.Init(visualElement, attributes, creationContext);
                var element = (AspectRatioPanel)visualElement;
                
                element.AspectRatioX = Mathf.Max(1, _aspectRatioX.GetValueFromBag(attributes, creationContext));
                element.AspectRatioY = Mathf.Max(1, _aspectRatioY.GetValueFromBag(attributes, creationContext));
                element.BalanceX = Mathf.Clamp(_balanceX.GetValueFromBag(attributes, creationContext), 0, 100);
                element.BalanceY = Mathf.Clamp(_balanceY.GetValueFromBag(attributes, creationContext), 0, 100);
                element.FitToParent();
            }
        }
    }
}