using ArPaint.UI.Elements.EventInterfaces;
using ArPaint.Utils;
using UIComponents;
using UnityEngine;
using UnityEngine.UIElements;

namespace ArPaint.UI.Elements
{
    [UxmlName("DropdownButton")]
    [Layout("UI/DrawView/DropdownButton")]
    public partial class DropdownButton : UIComponent, IOnGeometryChanged, IOnFocusOut
    {
        private const int ScreenHeight = 1920;
        
        [Query(Class = "dropdown-button__container")]
        public VisualElement container;
        
        [Query(Class = "dropdown-button__button")]
        public Button button;
        
        public override void OnInit()
        {
            button.clicked += OnButtonClicked;
            UpdateContainerOffset();
        }

        private void OnButtonClicked()
        {
            container.ToggleDisplay();
        }

        public void OnGeometryChanged(GeometryChangedEvent evt)
        {
            UpdateContainerOffset();
        }

        public void OnFocusOut(FocusOutEvent evt)
        {
            container.style.display = DisplayStyle.None;
        }

        private void UpdateContainerOffset()
        {
            var buttonHeight = button.layout.height;
            container.style.height = ScreenHeight - buttonHeight;
            container.style.translate = new Translate
            {
                y = new Length(-buttonHeight)
            };
        }
    }
}