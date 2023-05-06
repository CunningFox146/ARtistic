using System;
using System.Collections.Generic;
using ArPaint.UI.ViewModels.Draw;
using ArPaint.UI.ViewModels.DrawOptions;
using ArPaint.UI.Views.DrawOptions;
using UnityEngine;
using UnityEngine.UIElements;
using UnityMvvmToolkit.UITK;
using Zenject;

namespace ArPaint.UI.Views.Draw
{
    public class DrawView : View<DrawViewModel>
    {
        [SerializeField] private VisualTreeAsset _shapeViewAsset;

        [Inject]
        public void Constructor(DrawViewModel optionsViewModel)
        {
            viewModel = optionsViewModel;
        }

        protected override IReadOnlyDictionary<Type, object> GetCollectionItemTemplates()
        {
            return new Dictionary<Type, object>
            {
                { typeof(ShapeViewModel), _shapeViewAsset }
            };
        }
        
        public class Factory : PlaceholderFactory<DrawView>
        {
        }
    }
}