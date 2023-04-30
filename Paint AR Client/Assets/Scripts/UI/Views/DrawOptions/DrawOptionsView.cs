using ArPaint.UI.ViewModels.DrawOptions;
using UnityEngine;
using UnityEngine.UIElements;
using UnityMvvmToolkit.Core.Interfaces;
using Zenject;

namespace ArPaint.UI.Views.DrawOptions
{
    public class DrawOptionsView : View<DrawOptionsViewModel>
    {
        [SerializeField] private VisualTreeAsset _shapeViewAsset;

        [Inject]
        public void Constructor(DrawOptionsViewModel optionsViewModel)
        {
            ViewModel = optionsViewModel;
        }

        protected override IBindableElementsFactory GetBindableElementsFactory()
        {
            return new DrawOptionsViewBindableElementsFactory(_shapeViewAsset);
        }

        public class Factory : PlaceholderFactory<DrawOptionsView>
        {
        }
    }
}