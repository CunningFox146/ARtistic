using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ArPaint.Infrastructure.AssetProvider
{
    public interface IAssetProvider
    {
        T LoadAsset<T>(string path) where T : Object;
        UniTask<T> LoadAssetAsync<T>(string path) where T : Object;
    }
}