using System;
using System.Collections.Generic;
using ArPaint.UI.ViewModels.Discover;
using ArPaint.UI.ViewModels.Home;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace ArPaint.UI.Views.Discover
{
    public class DiscoverView : View<DiscoverViewModel>
    {
        [SerializeField] private VisualTreeAsset _drawingListViewAsset;

        protected override IReadOnlyDictionary<Type, object> GetCollectionItemTemplates()
        {
            return new Dictionary<Type, object>
            {
                { typeof(DrawingViewModel), _drawingListViewAsset }
            };
        }
        
        public class Factory : PlaceholderFactory<DiscoverView> {}
    }
}