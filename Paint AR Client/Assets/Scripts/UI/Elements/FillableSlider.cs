
using UnityEngine;
using UnityEngine.UIElements;

namespace ArPaint.UI.Elements
{
    public class FillableSlider : Slider
    {
        public readonly string progressBarClass = $"{ussClassName}__progress";

        private readonly VisualElement _progressBar;
        private readonly VisualElement _dragger;
        
        public FillableSlider() : base()
        {
            _progressBar = new VisualElement();
            _progressBar.usageHints = UsageHints.DynamicTransform;
            _progressBar.name = "progress";
            _progressBar.AddToClassList(progressBarClass);
            _progressBar.style.height = Length.Percent(100f);

            _dragger = this.Q<VisualElement>(className: draggerUssClassName);
            
            var tracker = this.Q<VisualElement>(className: trackerUssClassName);
            tracker.Add(_progressBar);
            
            
            _dragger.RegisterCallback<GeometryChangedEvent>(_ => UpdateFill());
            this.RegisterValueChangedCallback(_ => UpdateFill());
            UpdateFill();
        }

        private void UpdateFill()
        {
            _progressBar.style.width = _dragger.transform.position.x + _dragger.contentRect.width * 0.5f;
        }
        
        public new class UxmlTraits : Slider.UxmlTraits
        {
        }
        
        public new class UxmlFactory : UxmlFactory<FillableSlider, UxmlTraits>
        {
        }
    }
}