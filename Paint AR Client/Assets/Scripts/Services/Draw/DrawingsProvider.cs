using System;
using System.Collections.ObjectModel;
using ArPaint.Utils;
using Firebase.Firestore;
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

        public DrawingsProvider(IPersistentData persistentData, FirebaseFirestore firestore)
        {
            _persistentData = persistentData;

            var json = _persistentData.GetValue(nameof(Drawings));
            var loaded = !string.IsNullOrEmpty(json)
                ? JsonConvert.DeserializeObject<ObservableCollection<DrawingData>>(json)
                : null;

            Drawings = loaded ?? new ObservableCollection<DrawingData>();

            foreach (var drawing in Drawings)
            {
                firestore.Document($"drawings/{drawing.Id}").SetAsync(drawing);
            }
        }

        public DrawingData CreateNewData()
        {
            var data = new DrawingData()
            {
                Id = Guid.NewGuid().GetHashCode()
            };

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

        public void UpdateDrawing(DrawingData drawing)
        {
            Drawings.Update(drawing);
        }
    }
}