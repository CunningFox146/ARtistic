using System.Collections.ObjectModel;
using Newtonsoft.Json;
using Services.PersistentData;

namespace ArPaint.Services.Draw
{
    public class DrawingsProvider : IDrawingsProvider
    {
        private readonly IPersistentData _persistentData;
        public DrawingData SelectedDrawing { get; set; }
        public ObservableCollection<DrawingData> Drawings { get; }

        public DrawingsProvider(IPersistentData persistentData)
        {
            _persistentData = persistentData;

            var json = _persistentData.GetValue(nameof(Drawings));
            Drawings = json != null
                ? JsonConvert.DeserializeObject(json) as ObservableCollection<DrawingData>
                : new ObservableCollection<DrawingData>();
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

        public void Save()
        {
            var json = JsonConvert.SerializeObject(Drawings);
            _persistentData.SetValue(nameof(Drawings), json);
        }
    }
}