using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ArPaint.Infrastructure.AssetProvider
{
    public class ResourcesAssetProvider : IAssetProvider
    {
        public T LoadAsset<T>(string path) where T : Object
        {
            return Resources.Load<T>(path);
        }

        public async UniTask<T> LoadAssetAsync<T>(string path) where T : Object
        {
            return await Resources.LoadAsync<T>(path) as T;
        }
    }
}