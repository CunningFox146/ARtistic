using UIComponents.InterfaceModifiers;
using UnityEngine.UIElements;

namespace ArPaint.UI.Elements.EventInterfaces
{
    [RegistersEventCallback(typeof(FocusOutEvent))]
    public interface IOnFocusOut
    {
        void OnFocusOut(FocusOutEvent evt);
    }
}