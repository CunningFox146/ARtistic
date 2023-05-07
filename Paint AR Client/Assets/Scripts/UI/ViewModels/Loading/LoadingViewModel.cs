using System.Collections.Generic;
using System.Linq;
using ArPaint.Infrastructure.AssetProvider;
using Services.StaticData;
using UnityEngine;
using UnityMvvmToolkit.Core;
using UnityMvvmToolkit.Core.Attributes;
using UnityMvvmToolkit.Core.Interfaces;

namespace ArPaint.UI.ViewModels.Loading
{
    public class LoadingViewModel : ViewModel
    {
        private readonly IStaticDataService _staticDataService;

        [Observable(nameof(LoadingSprites))]
        private readonly IReadOnlyProperty<IList<Sprite>> _loadingSprites;

        public IList<Sprite> LoadingSprites => _loadingSprites.Value;
        
        public LoadingViewModel(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
            _loadingSprites = new ReadOnlyProperty<IList<Sprite>>(staticDataService.LoadingIconSprites);
        }
    }
}