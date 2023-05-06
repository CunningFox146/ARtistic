using ArPaint.UI.ViewModels.DrawOptions;
using UnityEngine.UIElements;
using UnityMvvmToolkit.UITK.BindableUIElements;

namespace ArPaint.UI.Views.DrawOptions
{
    public class ShapeListView : BindableListView<ShapeViewModel>
    {
        public new class UxmlFactory : UxmlFactory<ShapeListView, UxmlTraits>
        {
            public override string uxmlName => nameof(ShapeListView);
        }
    }
}