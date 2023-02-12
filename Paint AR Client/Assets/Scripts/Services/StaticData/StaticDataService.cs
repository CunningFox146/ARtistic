using ArPaint.Infrastructure.AssetProvider;
using Cysharp.Threading.Tasks;
using ArPaint.StaticData;

namespace Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string AssetsResourcesPath = "AssetsPath";
        private readonly IAssetProvider _assetsProvider;
        
        public AssetsPath AssetPath { get; private set; }

        public StaticDataService(IAssetProvider assetsProvider)
        {
            _assetsProvider = assetsProvider;
        }
        
        public async UniTask Load()
        {
            AssetPath = await _assetsProvider.LoadAssetAsync<AssetsPath>(AssetsResourcesPath);
            // TODO: Load brushes
        }
    }
}