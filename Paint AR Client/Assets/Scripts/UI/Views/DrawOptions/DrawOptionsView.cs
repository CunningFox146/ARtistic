using System;
using System.Collections.Generic;
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
            viewModel = optionsViewModel;
        }

        protected override IReadOnlyDictionary<Type, object> GetCollectionItemTemplates()
        {
            return new Dictionary<Type, object>
            {
                { typeof(ShapeViewModel), _shapeViewAsset }
            };
        }
        
        public class Factory : PlaceholderFactory<DrawOptionsView>
        {
        }
    }
}