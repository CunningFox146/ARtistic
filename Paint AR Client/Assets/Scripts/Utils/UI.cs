using UnityEngine.UIElements;

namespace ArPaint.Utils
{
    public static class UI
    {
        public static void Hide(this VisualElement visualElement)
        {
            visualElement.style.display = DisplayStyle.None;
            visualElement.style.visibility = Visibility.Hidden;
        }
        
        public static void Show(this VisualElement visualElement)
        {
            visualElement.style.display = DisplayStyle.Flex;
            visualElement.style.visibility = Visibility.Visible;
        }
        
        public static void ToggleDisplay(this VisualElement visualElement)
        {
            if (visualElement.style.display.value is DisplayStyle.Flex)
                visualElement.Hide();
            else
                visualElement.Show();

        }

        public static VisualElement GetDocumentRoot(this VisualElement element)
        {
            while (element.parent != null)
            {
                element = element.parent;
            }
            return element;
        }
    }
}