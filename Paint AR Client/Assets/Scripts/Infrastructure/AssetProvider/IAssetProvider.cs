using System.Threading.Tasks;
using UnityEngine;

namespace ArPaint.Infrastructure.AssetProvider
{
    public interface IAssetProvider
    {
        T LoadAsset<T>(string path) where T : Object;
        Task<T> LoadAssetAsync<T>(string path) where T : Object;
    }
}