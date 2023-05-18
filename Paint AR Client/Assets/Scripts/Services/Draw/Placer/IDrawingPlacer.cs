using System;

namespace ArPaint.Services.Draw.Placer
{
    public interface IDrawingPlacer
    {
        public event Action<bool> PlacementValidChanged;
        public bool IsPlacementValid { get; }
        void StartPlacing();
        void StopPlacing(bool disableContainer = false);
    }
}