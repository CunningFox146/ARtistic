using System;
using System.Collections.ObjectModel;

namespace ArPaint.Services.Draw
{
    public interface IDrawingsProvider
    {
        event Action<DrawingData> SelectedDrawingChanged;
        DrawingData SelectedDrawing { get; }
        ObservableCollection<DrawingData> Drawings { get; }
        DrawingData CreateNewData();
        void RemoveData(DrawingData data);
        void SelectDrawing(DrawingData drawingData, bool noNotify = false);
        void Save();
    }
}