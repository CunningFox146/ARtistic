using ArPaint.UI.ViewModels.Draw;
using UnityEngine;
using UnityEngine.UIElements;
using UnityMvvmToolkit.Core.Interfaces;
using Zenject;

namespace ArPaint.UI.Views.Draw
{
    public class DrawView : View<DrawViewModel>
    {
        [SerializeField] private VisualTreeAsset _shapeViewAsset;

        [Inject]
        public void Constructor(DrawViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        protected override IBindableElementsFactory GetBindableElementsFactory()
        {
            return new DrawViewBindableElementsFactory(_shapeViewAsset);
        }

        public class Factory : PlaceholderFactory<DrawView>
        {
        }
    }
}