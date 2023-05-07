using UnityEngine;

namespace ArPaint.StaticData
{
    [CreateAssetMenu(menuName = "Static Data/Assets Path", fileName = "AssetsPath")]
    public class AssetsPath : ScriptableObject, IAssetsPath
    {
        [field: SerializeField] public string DrawLinePath { get; private set; }
        [field: SerializeField] public string ViewPrefabsPath { get; private set; }
    }
}