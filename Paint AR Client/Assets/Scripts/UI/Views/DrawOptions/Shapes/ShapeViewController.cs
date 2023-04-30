using System;
using ArPaint.UI.ViewModels.DrawOptions;
using UnityEngine.UIElements;

namespace ArPaint.UI.Views.DrawOptions.Shapes
{
    public class ShapeViewController : IDisposable
    {
        private readonly Button _button;
        private readonly Action<ShapeViewData> _onClick;
        private ShapeViewData _data;

        public ShapeViewController(VisualElement userEntryAsset, Action<ShapeViewData> onClick)
        {
            _button = userEntryAsset.Q<Button>();
            _button.clicked += OnClick;

            _onClick = onClick;
        }

        public void Dispose()
        {
            _button.clicked -= OnClick;
        }

        private void OnClick()
        {
            _onClick?.Invoke(_data);
        }

        public void SetData(ShapeViewData data)
        {
            _data = data;
            _button.text = _data.Shape.Name;
        }
    }
}