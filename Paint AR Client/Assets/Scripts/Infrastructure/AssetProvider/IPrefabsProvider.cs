using UnityEngine;

namespace ArPaint.Infrastructure.AssetProvider
{
    public interface IPrefabsProvider
    {
        GameObject LoadLinePrefab { get; }
    }
}