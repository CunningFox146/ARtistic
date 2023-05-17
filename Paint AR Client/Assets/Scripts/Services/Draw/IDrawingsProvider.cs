using System.Collections.ObjectModel;

namespace ArPaint.Services.Draw
{
    public interface IDrawingsProvider
    {
        DrawingData SelectedDrawing { get; set; }
        ObservableCollection<DrawingData> Drawings { get; }
        DrawingData CreateNewData();
        void RemoveData(DrawingData data);
        void Save();
    }
}