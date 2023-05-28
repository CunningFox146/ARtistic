using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ArPaint.Utils;
using Cysharp.Threading.Tasks;
using Firebase.Firestore;
using Newtonsoft.Json;
using Services.Auth;
using Services.ImageProvider;
using Services.PersistentData;
using Unity.VisualScripting;
using UnityEngine;

namespace ArPaint.Services.Draw
{
    public class DrawingsProvider : IDrawingsProvider
    {
        private const string CollectionName = "drawings";
        public event Action<DrawingData> SelectedDrawingChanged;
        
        private readonly IPersistentData _persistentData;
        private readonly FirebaseFirestore _firestore;
        private readonly IAuthSystem _auth;
        private readonly IImageProvider _imageProvider;
        public DrawingData SelectedDrawing { get; private set; }
        public ObservableCollection<DrawingData> Drawings { get; }

        public DrawingsProvider(IPersistentData persistentData, FirebaseFirestore firestore, IAuthSystem auth, IImageProvider imageProvider)
        {
            _persistentData = persistentData;
            _firestore = firestore;
            _auth = auth;
            _imageProvider = imageProvider;

            Drawings = new();
            
            Reload();
        }

        public void Reload()
        {
            Drawings.Clear();
            var json = _persistentData.GetValue(nameof(Drawings));
            var loaded = !string.IsNullOrEmpty(json)
                ? JsonConvert.DeserializeObject<ObservableCollection<DrawingData>>(json)
                : null;

            if (loaded != null)
                Drawings.AddRange(loaded);
        }

        public async UniTask UpdateOwnedItems()
        {
            var databaseItems = await GetPublishedDrawings();

            // Upload items that are Published, but not in databaseItems
            foreach (var drawing in Drawings.Where(drawing => drawing.IsPublished))
            {
                if (databaseItems.FirstOrDefault(databaseDrawing => databaseDrawing.Id == drawing.Id) == null)
                {
                    await UploadDrawing(drawing);
                }
            }
            
            // Download items that have current user's id and are not in Drawings
            var drawingsToAdd = new List<DrawingData>();
            foreach (var drawing in databaseItems.Where(drawing => drawing.Author == _auth.User.UserId))
            {
                if (Drawings.FirstOrDefault(localDrawing => localDrawing.Id == drawing.Id) == null)
                {
                    drawing.IsPublished = true;
                    drawing.IsOwned = true;
                    drawingsToAdd.Add(drawing);
                }
            }
            
            Drawings.AddRange(drawingsToAdd);
        }

        public async UniTask UploadDrawing(DrawingData drawing)
        {
            drawing.IsPublished = true;
            await Save();
        }

        public async UniTask UnUploadDrawing(DrawingData drawing, bool noSave = false)
        {
            drawing.IsPublished = false;
            await _firestore.Document($"{CollectionName}/drawing_{drawing.Id}").DeleteAsync();
            await _imageProvider.DeleteImage(drawing.Id.ToString());
            
            if (!noSave)
                await Save();
        }

        public async UniTask<List<DrawingData>> GetPublishedDrawings()
        {
            var drawings = new List<DrawingData>();
            var query = await _firestore.Collection(CollectionName).GetSnapshotAsync();
            
            foreach (var document in query.Documents)
            {
                var data = document.ConvertTo<DrawingData>();
                data.IsOwned = data.Author == _auth.User.UserId;
                drawings.Add(data);
            }

            return drawings;
        }

        public DrawingData CreateNewData()
        {
            var data = new DrawingData
            {
                Id = Guid.NewGuid().GetHashCode(),
                IsOwned = true,
                Author = _auth.User.UserId,
                AuthorName = _auth.User.DisplayName,
                CreationDate = DateTime.Today
            };

            Drawings.Add(data);

            return data;
        }

        public async UniTask RemoveData(DrawingData data)
        {
            Drawings.Remove(data);
            await UnUploadDrawing(data, true);
        }

        public void SelectDrawing(DrawingData drawingData, bool noNotify = false)
        {
            SelectedDrawing = drawingData;
            if (!noNotify)
                SelectedDrawingChanged?.Invoke(SelectedDrawing);
        }

        public async UniTask Save()
        {
            var json = JsonConvert.SerializeObject(Drawings);
            _persistentData.SetValue(nameof(Drawings), json);

            foreach (var drawing in Drawings.Where(drawing => drawing.IsPublished))
            {
                await _firestore.Document($"{CollectionName}/drawing_{drawing.Id}").SetAsync(drawing);
            }
        }

        public void UpdateDrawing(DrawingData drawing)
        {
            Drawings.Update(drawing);
        }
    }
}