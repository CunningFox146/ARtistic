using UnityEngine;

namespace ArPaint.StaticData
{
    [CreateAssetMenu(fileName = "Static Data/Assets Path")]
    public class AssetsPath : ScriptableObject
    {
        [field: SerializeField] public string DrawLinePath { get; private set; }
    }
}