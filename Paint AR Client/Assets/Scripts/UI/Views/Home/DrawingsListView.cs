using ArPaint.UI.ViewModels.Draw;
using ArPaint.UI.ViewModels.Home;
using UnityEngine.UIElements;
using UnityMvvmToolkit.UITK.BindableUIElements;

namespace ArPaint.UI.Views.Home
{
    public class DrawingsListView : BindableListView<DrawingViewModel>
    {
        public new class UxmlFactory : UxmlFactory<DrawingsListView, UxmlTraits>
        {
            public override string uxmlName => nameof(DrawingsListView);
        }
    }
}