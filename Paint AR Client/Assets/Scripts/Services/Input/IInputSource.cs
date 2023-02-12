using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem.Utilities;

namespace ArPaint.Services.Input
{
    public interface IInputSource
    {
        ReadOnlyArray<Touch> Touches { get; }
    }
}