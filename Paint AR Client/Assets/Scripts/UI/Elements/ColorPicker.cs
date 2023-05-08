﻿using System;
using ArPaint.Utils;
using ArPaint.Utils.Hsl;
using UnityEngine;
using UnityEngine.UIElements;

namespace ArPaint.UI.Elements
{
    public class ColorPicker : VisualElement
    {
        public event Action<Color> ColorChanged;
        
        private static readonly string ussClassName = "color-picker";
        private static readonly string colorPreviewClass = $"{ussClassName}__color-preview";
        private static readonly string sliderContainerClass = $"{ussClassName}__slider-container";
        private static readonly string colorSliderClass = $"{ussClassName}__color";
        private static readonly string lightnessSliderClass = $"{ussClassName}__lightness";
        private static readonly string alphaSliderClass = $"{ussClassName}__alpha";

        private readonly Slider _colorSlider;
        private readonly Slider _lightnessSlider;
        private readonly Slider _alphaSlider;
        
        private readonly VisualElement _colorPreview;
        private readonly VisualElement _slidersContainer;

        private ColorHsl _currentColor;
        
        public ColorPicker()
        {
            style.flexDirection = FlexDirection.Row;
            AddToClassList(ussClassName);

            _colorPreview = new VisualElement();
            _colorPreview.style.flexGrow = .25f;
            _colorPreview.style.backgroundColor = Color.white;
            _colorPreview.AddToClassList(colorPreviewClass);
            Add(_colorPreview);
            
            _slidersContainer = new VisualElement();
            _slidersContainer.style.flexGrow = .75f;
            _slidersContainer.AddToClassList(sliderContainerClass);
            Add(_slidersContainer);
            
            _colorSlider = CreateSlider();
            _colorSlider.AddToClassList(colorSliderClass);
            _colorSlider.RegisterValueChangedCallback(OnColorSliderChange);
            
            _lightnessSlider = CreateSlider();
            _lightnessSlider.AddToClassList(lightnessSliderClass);
            _lightnessSlider.RegisterValueChangedCallback(OnLightnessSliderChange);
            
            _alphaSlider = CreateSlider();
            _alphaSlider.AddToClassList(alphaSliderClass);
            _alphaSlider.RegisterValueChangedCallback(OnAlphaSliderChange);

            _slidersContainer.Add(_colorSlider);
            _slidersContainer.Add(_lightnessSlider);
            _slidersContainer.Add(_alphaSlider);
            
            SetColor(Color.white);
        }

        public void SetColor(Color color)
        {
            _currentColor = color.ToHSL();
            OnColorChanged();
            
            _colorSlider.SetValueWithoutNotify(_currentColor.H / 360f);
            _lightnessSlider.SetValueWithoutNotify(_currentColor.L);
            _alphaSlider.SetValueWithoutNotify(_currentColor.A);
        }

        private void OnColorSliderChange(ChangeEvent<float> evt)
        {
            _currentColor = _currentColor.SetH(Mathf.FloorToInt(evt.newValue * 360f));
            UnityEngine.Debug.Log(_currentColor);
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

        private void OnColorChanged()
        {
            var color = _currentColor.ToRGB();
            var tintColor = new ColorHsl(_currentColor.H, 1f, 0.5f).ToRGB();
            _lightnessSlider.Q(className:"unity-base-slider__tracker").style.unityBackgroundImageTintColor = tintColor;
            _alphaSlider.Q(className:"unity-base-slider__tracker").style.unityBackgroundImageTintColor = tintColor;
            
            ColorChanged?.Invoke(color);
            _colorPreview.style.backgroundColor = color;
        }

        private static Slider CreateSlider()
        {
            return new Slider
            {
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
            
        }
    }
}