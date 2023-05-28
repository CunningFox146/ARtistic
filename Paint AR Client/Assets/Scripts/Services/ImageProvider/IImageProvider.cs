using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Services.ImageProvider
{
    public interface IImageProvider
    {
        UniTask<Texture2D> LoadImage(string path, int maxDownloadSize = 512 * 512);
        UniTask UploadImage(string path, Texture texture);
        UniTask DeleteImage(string path);
    }
}