using ArPaint.UI.Views.Draw.Shapes;
using UnityEngine.UIElements;
using UnityMvvmToolkit.Core.Interfaces;
using UnityMvvmToolkit.UGUI;
using UnityMvvmToolkit.UITK.BindableUIElements;

namespace ArPaint.UI.Views.Draw
{
    public class DrawViewBindableElementsFactory : BindableElementsFactory
    {
        private readonly VisualTreeAsset _shapeViewAsset;

        public DrawViewBindableElementsFactory(VisualTreeAsset shapeViewAsset)
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