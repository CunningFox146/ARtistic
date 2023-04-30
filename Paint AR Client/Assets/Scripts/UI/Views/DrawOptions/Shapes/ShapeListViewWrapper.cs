using ArPaint.UI.ViewModels.DrawOptions;
using UnityEngine.UIElements;
using UnityMvvmToolkit.Core.Interfaces;
using UnityMvvmToolkit.UITK.BindableUIElements;
using UnityMvvmToolkit.UITK.BindableUIElementWrappers;
using Utils;

namespace ArPaint.UI.Views.DrawOptions.Shapes
{
    public class ShapeListViewWrapper : BindableListViewWrapper<ShapeViewController, ShapeViewData>
    {
        public ShapeListViewWrapper(BindableListView listView, VisualTreeAsset itemAsset, IObjectProvider objectProvider) : base(listView, itemAsset, objectProvider)
        {
        }

        protected override ShapeViewController OnMakeItem(VisualElement itemAsset)
        {
            return new(itemAsset, OnShapeBecameActive);
        }

        protected override void OnBindItem(ShapeViewController item, ShapeViewData data)
        {
            item.SetData(data);
        }

        private void OnShapeBecameActive(ShapeViewData shape)
        {
            foreach (var data in ItemsCollection)
            {
                data.IsSelected = data == shape;
            }
            ItemsCollection.Update(shape);
        }
    }
}