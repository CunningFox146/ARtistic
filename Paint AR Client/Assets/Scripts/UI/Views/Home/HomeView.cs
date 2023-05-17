using System;
using System.Collections.Generic;
using ArPaint.UI.ViewModels.Home;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace ArPaint.UI.Views.Home
{
    public class HomeView : View<HomeViewModel>
    {
        [SerializeField] private VisualTreeAsset _drawingViewAsset;

        protected override IReadOnlyDictionary<Type, object> GetCollectionItemTemplates()
        {
            return new Dictionary<Type, object>
            {
                { typeof(DrawingsListView), _drawingViewAsset }
            };
        }
        
        public class Factory : PlaceholderFactory<HomeView>
        {
        }
    }
}