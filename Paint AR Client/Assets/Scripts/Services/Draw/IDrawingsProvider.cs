using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Cysharp.Threading.Tasks;

namespace ArPaint.Services.Draw
{
    public interface IDrawingsProvider
    {
        event Action<DrawingData> SelectedDrawingChanged;
        DrawingData SelectedDrawing { get; }
        ObservableCollection<DrawingData> Drawings { get; }
        UniTask<List<DrawingData>> GetPublishedDrawings();
        UniTask UpdateOwnedItems();
        DrawingData CreateNewData();
        UniTask RemoveData(DrawingData data);
        void SelectDrawing(DrawingData drawingData, bool noNotify = false);
        UniTask Save();
        void UpdateDrawing(DrawingData drawing);
        UniTask UploadDrawing(DrawingData drawing);
        UniTask UnUploadDrawing(DrawingData drawing, bool noSave = false);
        void Reload();
    }
}