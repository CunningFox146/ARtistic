using System;
using ArPaint.Infrastructure.GameLoop;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem.Utilities;

namespace ArPaint.Services.Input
{
    public class InputSource : IDisposable, IInputSource
    {
        private readonly IUpdateLoop _updateLoop;

        public ReadOnlyArray<Touch> Touches => Touch.activeTouches;

        public InputSource()
        {
            EnhancedTouchSupport.Enable();
        }

        public void Dispose()
        {
            EnhancedTouchSupport.Disable();
        }
    }
}