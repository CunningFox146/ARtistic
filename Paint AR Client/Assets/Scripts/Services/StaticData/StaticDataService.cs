using ArPaint.Infrastructure.AssetProvider;
using ArPaint.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string AssetsResourcesPath = "StaticData";
        private readonly IAssetProvider _assetsProvider;

        public IAssetsPath AssetPath { get; private set; }
        public IShapes Shapes { get; private set; }

        public StaticDataService(IAssetProvider assetsProvider)
        {
            _assetsProvider = assetsProvider;
        }

        public async UniTask Load()
        {
            AssetPath = await LoadAssetAsync<AssetsPath>();
            Shapes = await LoadAssetAsync<Shapes>();
            // TODO: Load brushes
        }

        private async UniTask<T> LoadAssetAsync<T>() where T : Object
        {
            return await _assetsProvider.LoadAssetAsync<T>($"{AssetsResourcesPath}/{typeof(T).Name}");
        }
    }
}