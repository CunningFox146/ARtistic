using System;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using Services.PersistentData;

namespace ArPaint.Services.Draw
{
    public class DrawingsProvider : IDrawingsProvider
    {
        public event Action<DrawingData> SelectedDrawingChanged;
        
        private readonly IPersistentData _persistentData;
        public DrawingData SelectedDrawing { get; private set; }
        public ObservableCollection<DrawingData> Drawings { get; }

        public DrawingsProvider(IPersistentData persistentData)
        {
            _persistentData = persistentData;

            var json = _persistentData.GetValue(nameof(Drawings));
            Drawings = json != null
                ? JsonConvert.DeserializeObject(json) as ObservableCollection<DrawingData>
                : new ObservableCollection<DrawingData>();

            for (int i = 0; i < 5; i++)
            {
                Drawings.Add(new()
                {
                    Description = $"Item {i}",
                    Name = $"WOWZA {i}"
                });
            }
        }

        public DrawingData CreateNewData()
        {
            var data = new DrawingData();

            Drawings.Add(data);

            return data;
        }

        public void RemoveData(DrawingData data)
        {
            Drawings.Remove(data);
        }

        public void SelectDrawing(DrawingData drawingData, bool noNotify = false)
        {
            SelectedDrawing = drawingData;
            if (!noNotify)
                SelectedDrawingChanged?.Invoke(SelectedDrawing);
        }

        public void Save()
        {
            var json = JsonConvert.SerializeObject(Drawings);
            _persistentData.SetValue(nameof(Drawings), json);
        }
    }
}