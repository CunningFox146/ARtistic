using System.Collections.Generic;
using ArPaint.Utils;
using Cysharp.Threading.Tasks;
using Firebase.Storage;
using UnityEngine;

namespace Services.ImageProvider
{
    public class ImageProvider : IImageProvider
    {
        private const string ImagesPath = "previews";
        private static readonly Vector2Int PreviewSize = new(128, 128);

        private readonly FirebaseStorage _storage;
        private readonly Dictionary<string, Texture2D> _cache = new();

        private static string ImagePath(string path) => $"{ImagesPath}/{path}.png";

        public ImageProvider(FirebaseStorage storage)
        {
            _storage = storage;
        }

        public async UniTask<Texture2D> LoadImage(string path, int maxDownloadSize = 512 * 512)
        {
            if (_cache.TryGetValue(path, out var cachedTexture))
                return cachedTexture;
            
            var storageReference = _storage.GetReference(ImagePath(path));
            var bytes = await storageReference.GetBytesAsync(maxDownloadSize);
            var texture = new Texture2D(1, 1);
            texture.LoadImage(bytes);
            _cache[path] = texture;
            return texture;
        }

        public async UniTask UploadImage(string path, Texture texture)
        {
            var bytes = await texture.ToBytesArray(widthOverride: (uint)PreviewSize.x, heightOverride: (uint)PreviewSize.y);
            if (bytes != null)
            {
                var storageReference = _storage.RootReference.Child(ImagePath(path));
                await storageReference.PutBytesAsync(bytes);
            }
        }
        
        public async UniTask DeleteImage(string path)
        {
            var storageReference = _storage.GetReference(ImagePath(path));
            await storageReference.DeleteAsync();
            _cache.Remove(path);
        }
    }
}