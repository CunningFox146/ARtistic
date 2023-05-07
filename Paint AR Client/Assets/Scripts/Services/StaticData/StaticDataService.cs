using System.Collections.Generic;
using System.Threading.Tasks;
using ArPaint.Infrastructure.AssetProvider;
using ArPaint.StaticData;
using Cysharp.Threading.Tasks;
using StaticData;
using UnityEngine;
using UnityEngine.U2D;

namespace Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string AssetsResourcesPath = "StaticData";
        private readonly IAssetProvider _assetsProvider;

        public IAssetsPath AssetPath { get; private set; }
        public IShapes Shapes { get; private set; }
        public IList<Sprite> LoadingIconSprites { get; private set; }

        public StaticDataService(IAssetProvider assetsProvider)
        {
            _assetsProvider = assetsProvider;
        }

        public async UniTask Load()
        {
            AssetPath = await LoadAssetAsync<AssetsPath>();
            Shapes = await LoadAssetAsync<Shapes>();
            LoadingIconSprites = await LoadLoadingIcon();
            // TODO: Load brushes
        }

        private async Task<List<Sprite>> LoadLoadingIcon()
        {
            var sprites = await LoadAssetAsync<LoadingSprites>();
            return sprites.LoadingSpritesList;
        }

        private async UniTask<T> LoadAssetAsync<T>() where T : Object
        {
            return await _assetsProvider.LoadAssetAsync<T>($"{AssetsResourcesPath}/{typeof(T).Name}");
        }
    }
}