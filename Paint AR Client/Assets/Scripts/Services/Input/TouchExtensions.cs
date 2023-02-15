using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace ArPaint.Services.Input
{
    public static class TouchExtensions
    {
        private static readonly List<RaycastResult> RaycastResults = new();

        public static Vector3 GetWorldPosition(this Touch touch, Camera camera, float offset = 0f)
        {
            return camera.ScreenToWorldPoint(new Vector3(touch.screenPosition.x, touch.screenPosition.y,
                camera.nearClipPlane + offset));
        }

        public static bool IsOverUI(this Touch touch)
        {
            RaycastResults.Clear();
            var eventDataCurrentPosition = new PointerEventData(EventSystem.current)
            {
                position = new Vector2(touch.screenPosition.x, touch.screenPosition.y)
            };
            EventSystem.current.RaycastAll(eventDataCurrentPosition, RaycastResults);
            return RaycastResults.Any();
        }
    }
}