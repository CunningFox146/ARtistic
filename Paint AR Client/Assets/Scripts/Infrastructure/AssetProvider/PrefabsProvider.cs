using Services.StaticData;
using UnityEngine;

namespace ArPaint.Infrastructure.AssetProvider
{
    public class PrefabsProvider : IPrefabsProvider
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticDataService;

        public GameObject ShapeContainerPrefab =>
            _assetProvider.LoadAsset<GameObject>(_staticDataService.AssetPath.DrawLinePath);

        public GameObject DrawViewPrefab => 
            _assetProvider.LoadAsset<GameObject>(_staticDataService.AssetPath.DrawViewPath);
        
        public PrefabsProvider(IAssetProvider assetProvider, IStaticDataService staticDataService)
        {
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
        }
    }
}