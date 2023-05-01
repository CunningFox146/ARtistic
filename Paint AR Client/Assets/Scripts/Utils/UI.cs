using UnityEngine.UIElements;

namespace ArPaint.Utils
{
    public static class UI
    {
        public static void ToggleDisplay(this VisualElement visualElement)
        {
            visualElement.style.display = visualElement.style.display.value == DisplayStyle.Flex
                ? DisplayStyle.None
                : DisplayStyle.Flex;
        }
        
        
    }
}