using UIComponents.InterfaceModifiers;
using UnityEngine.UIElements;

namespace ArPaint.UI.Elements.EventInterfaces
{
    [RegistersEventCallback(typeof(FocusInEvent))]
    public interface IOnFocusIn
    {
        void OnFocusIn(FocusInEvent evt);
    }
}