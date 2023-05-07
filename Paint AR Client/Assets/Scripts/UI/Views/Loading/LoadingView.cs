using System.Collections.Generic;
using System.Threading.Tasks;
using ArPaint.UI.Elements;
using ArPaint.UI.ViewModels.Loading;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace ArPaint.UI.Views.Loading
{
    public class LoadingView : View<LoadingViewModel>
    {
        [SerializeField] private List<Sprite> _loadingIconSprites;

        protected override void OnInit()
        {
            base.OnInit();

            RootVisualElement.Q<LoadingIconView>().SetSprites(_loadingIconSprites);
        }
        
        public class Factory : PlaceholderFactory<LoadingView>
        {
        }
    }
}