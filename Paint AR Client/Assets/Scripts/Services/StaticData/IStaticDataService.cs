using ArPaint.StaticData;
using Cysharp.Threading.Tasks;

namespace Services.StaticData
{
    public interface IStaticDataService
    {
        IAssetsPath AssetPath { get; }
        IShapes Shapes { get; }
        UniTask Load();
    }
}