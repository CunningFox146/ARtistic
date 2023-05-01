using UnityEngine;

namespace ArPaint.StaticData
{
    [CreateAssetMenu(fileName = "Static Data/Assets Path")]
    public class AssetsPath : ScriptableObject, IAssetsPath
    {
        [field: SerializeField] public string DrawLinePath { get; private set; }
        [field: SerializeField] public string ViewPrefabsPath { get; private set; }
    }
}