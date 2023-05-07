using System.Collections.Generic;
using UnityEngine;

namespace StaticData
{
    public interface ILoadingSprites
    {
        List<Sprite> LoadingSpritesList { get; }
    }
}