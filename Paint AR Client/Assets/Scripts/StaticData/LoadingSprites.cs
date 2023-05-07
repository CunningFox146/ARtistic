using System.Collections.Generic;
using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(menuName = "Static Data/Loading Sprites", fileName = "LoadingSprites")]
    public class LoadingSprites : ScriptableObject, ILoadingSprites
    {
        [field: SerializeField] public List<Sprite> LoadingSpritesList { get; private set; }
    }
}