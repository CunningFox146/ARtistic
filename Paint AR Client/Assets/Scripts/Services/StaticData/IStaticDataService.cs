using ArPaint.StaticData;
using Cysharp.Threading.Tasks;

namespace Services.StaticData
{
    public interface IStaticDataService
    {
        AssetsPath AssetPath { get; }
        UniTask Load();
    }
}