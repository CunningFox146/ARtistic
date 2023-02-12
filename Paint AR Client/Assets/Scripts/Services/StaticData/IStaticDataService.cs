using Cysharp.Threading.Tasks;
using ArPaint.StaticData;

namespace Services.StaticData
{
    public interface IStaticDataService
    {
        AssetsPath AssetPath { get; }
        UniTask Load();
    }
}