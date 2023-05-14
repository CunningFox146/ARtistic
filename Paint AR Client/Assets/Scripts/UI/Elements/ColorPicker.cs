using System;
using ArPaint.Utils;
using ArPaint.Utils.Hsl;
using UnityEngine;
using UnityEngine.UIElements;

namespace ArPaint.UI.Elements
{
    public class ColorPicker : VisualElement
    {
        private static readonly string ussClassName = "color-picker";
        private static readonly string colorPreviewClass = $"{ussClassName}__color-preview";
        private static readonly string sliderContainerClass = $"{ussClassName}__slider-container";
        private static readonly string colorSliderClass = $"{ussClassName}__color";
        private static readonly string lightnessSliderClass = $"{ussClassName}__lightness";
        private static readonly string saturationSliderClass = $"{ussClassName}__saturation";
        private static readonly string alphaSliderClass = $"{ussClassName}__alpha";
        private static readonly string sliderTrackerClass = "unity-base-slider__tracker";

        private readonly Slider _colorSlider;
        private readonly Slider _lightnessSlider;
        private readonly Slider _alphaSlider;
        private readonly Slider _saturationSlider;

        private readonly VisualElement _saturationSliderTracker;
        private readonly VisualElement _lightnessSliderTracker;
        private readonly VisualElement _alphaSliderTracker;

        private readonly VisualElement _colorPreview;
        private readonly VisualElement _slidersContainer;

        private ColorHsl _currentColor;


        private Color StartColor { get; set; } = Color.white;
        private Color TintColor => new ColorHsl(_currentColor.H, 1f, 0.5f).ToRGB();
        public Color CurrentColor => _currentColor.ToRGB();

        public ColorPicker()
        {
            style.flexDirection = FlexDirection.Row;
            AddToClassList(ussClassName);

            _colorPreview = new VisualElement();
            _colorPreview.name = "color-preview";
            _colorPreview.style.flexGrow = .25f;
            _colorPreview.style.backgroundColor = StartColor;
            _colorPreview.AddToClassList(colorPreviewClass);
            Add(_colorPreview);
            
            _slidersContainer = new VisualElement();
            _slidersContainer.name = "sliders-container";
            _slidersContainer.style.flexGrow = .75f;
            _slidersContainer.AddToClassList(sliderContainerClass);
            Add(_slidersContainer);
            
            _colorSlider = CreateSlider("color-slider");
            _colorSlider.AddToClassList(colorSliderClass);
            _colorSlider.RegisterValueChangedCallback(OnColorSliderChange);
            
            _lightnessSlider = CreateSlider("lightness-slider");
            _lightnessSlider.AddToClassList(lightnessSliderClass);
            _lightnessSlider.RegisterValueChangedCallback(OnLightnessSliderChange);
            _lightnessSliderTracker = _lightnessSlider.Q(className: sliderTrackerClass);
            
            _saturationSlider = CreateSlider("saturation-slider");
            _saturationSlider.AddToClassList(saturationSliderClass);
            _saturationSlider.RegisterValueChangedCallback(OnSaturationSliderChange);
            _saturationSliderTracker = _saturationSlider.Q(className: sliderTrackerClass);

            _alphaSlider = CreateSlider("alpha-slider");
            _alphaSlider.AddToClassList(alphaSliderClass);
            _alphaSlider.RegisterValueChangedCallback(OnAlphaSliderChange);
            _alphaSliderTracker = _alphaSlider.Q(className: sliderTrackerClass);

            _slidersContainer.Add(_colorSlider);
            _slidersContainer.Add(_lightnessSlider);
            _slidersContainer.Add(_saturationSlider);
            _slidersContainer.Add(_alphaSlider);
            
            SetColor(StartColor);
        }

        public void SetColor(Color color)
        {
            _currentColor = color.ToHSL();
            OnColorChanged();
            
            _colorSlider.SetValueWithoutNotify(_currentColor.H / 360f);
            _saturationSlider.SetValueWithoutNotify(_currentColor.S);
            _lightnessSlider.SetValueWithoutNotify(_currentColor.L);
            _alphaSlider.SetValueWithoutNotify(_currentColor.A);
        }

        private void OnColorSliderChange(ChangeEvent<float> evt)
        {
            _currentColor = _currentColor.SetH(Mathf.FloorToInt(evt.newValue * 360f));
            OnColorChanged();
        }

        private void OnSaturationSliderChange(ChangeEvent<float> evt)
        {
            _currentColor = _currentColor.SetS(evt.newValue);
            OnColorChanged();
        }

        private void OnLightnessSliderChange(ChangeEvent<float> evt)
        {
            _currentColor = _currentColor.SetL(evt.newValue);
            OnColorChanged();
        }

        private void OnAlphaSliderChange(ChangeEvent<float> evt)
        {
            _currentColor = _currentColor.SetA(evt.newValue);
            OnColorChanged();
        }

        protected virtual void OnColorChanged()
        {
            var color = _currentColor.ToRGB();
            
            _colorPreview.style.backgroundColor = color;
            _saturationSliderTracker.style.backgroundColor = TintColor;
            _lightnessSliderTracker.style.backgroundColor = TintColor;
            _alphaSliderTracker.style.unityBackgroundImageTintColor = TintColor;
        }

        private static Slider CreateSlider(string name)
        {
            return new Slider
            {
                name = name,
                highValue = 1f,
                style =
                {
                    flexGrow = 1f
                }
            };
        }

        public new class UxmlFactory : UxmlFactory<ColorPicker, UxmlTraits>
        {
        }

        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            private readonly UxmlColorAttributeDescription _bindingImageAttribute = new()
                { name = "start-color", defaultValue = Color.white };

            public override void Init(VisualElement visualElement, IUxmlAttributes bag, CreationContext context)
            {
                base.Init(visualElement, bag, context);
                var view = (ColorPicker)visualElement;
                    
                view.StartColor = _bindingImageAttribute.GetValueFromBag(bag, context);
            }
        }

    }
}