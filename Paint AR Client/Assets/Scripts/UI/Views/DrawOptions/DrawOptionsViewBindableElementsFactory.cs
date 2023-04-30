using ArPaint.UI.Views.DrawOptions.Shapes;
using UnityEngine.UIElements;
using UnityMvvmToolkit.Core.Interfaces;
using UnityMvvmToolkit.UGUI;
using UnityMvvmToolkit.UITK.BindableUIElements;

namespace ArPaint.UI.Views.DrawOptions
{
    public class DrawOptionsViewBindableElementsFactory : BindableElementsFactory
    {
        private readonly VisualTreeAsset _shapeViewAsset;

        public DrawOptionsViewBindableElementsFactory(VisualTreeAsset shapeViewAsset)
        {
            _shapeViewAsset = shapeViewAsset;
        }

        public override IBindableElement Create(IBindableUIElement bindableUiElement, IObjectProvider objectProvider)
        {
            return bindableUiElement switch
            {
                BindableListView listView => new ShapeListViewWrapper(listView, _shapeViewAsset, objectProvider),

                _ => base.Create(bindableUiElement, objectProvider)
            };
        }
    }
}