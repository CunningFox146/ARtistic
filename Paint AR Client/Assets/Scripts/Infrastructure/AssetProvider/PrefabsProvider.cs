using Services.StaticData;
using UnityEngine;

namespace ArPaint.Infrastructure.AssetProvider
{
    public class PrefabsProvider : IPrefabsProvider
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticDataService;

        public GameObject LoadLinePrefab =>
            _assetProvider.LoadAsset<GameObject>(_staticDataService.AssetPath.DrawLinePath);

        public PrefabsProvider(IAssetProvider assetProvider, IStaticDataService staticDataService)
        {
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
        }
    }
}