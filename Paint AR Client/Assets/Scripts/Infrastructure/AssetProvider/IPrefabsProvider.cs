using UnityEngine;

namespace ArPaint.Infrastructure.AssetProvider
{
    public interface IPrefabsProvider
    {
        GameObject ShapeContainerPrefab { get; }
        GameObject DrawViewPrefab { get; }
    }
}