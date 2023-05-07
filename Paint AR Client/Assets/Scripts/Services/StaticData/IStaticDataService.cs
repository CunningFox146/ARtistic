using System.Collections.Generic;
using ArPaint.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.U2D;

namespace Services.StaticData
{
    public interface IStaticDataService
    {
        IAssetsPath AssetPath { get; }
        IShapes Shapes { get; }
        IList<Sprite> LoadingIconSprites { get; }
        UniTask Load();
    }
}