using ArPaint.UI.ViewModels.Draw;
using UnityEngine.UIElements;
using UnityMvvmToolkit.UITK.BindableUIElements;

namespace ArPaint.UI.Views.Draw
{
    public class ShapeListView : BindableListView<ShapeViewModel>
    {
        public new class UxmlFactory : UxmlFactory<ShapeListView, UxmlTraits>
        {
            public override string uxmlName => nameof(ShapeListView);
        }
    }
}