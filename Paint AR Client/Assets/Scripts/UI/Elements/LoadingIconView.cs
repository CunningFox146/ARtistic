using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;
using UnityMvvmToolkit.Core;
using UnityMvvmToolkit.Core.Extensions;
using UnityMvvmToolkit.Core.Interfaces;

namespace ArPaint.UI.Elements
{
    public class LoadingIconView : Image, IBindableElement
    {
        private PropertyBindingData _spritesPathBindingData;
        private IReadOnlyProperty<IList<Sprite>> _spritesProperty;

        private int _currentFrame;
        private CancellationTokenSource _animationToken;
        
        public int FramesPerSecond { get; set; }
        public float FadeDuration { get; set; }
        public string SpritesBindingPath { get; set; }

        private IList<Sprite> Sprites => _spritesProperty.Value;
        private int FrameWaitTime => 1000 / FramesPerSecond;
        
        public void SetBindingContext(IBindingContext context, IObjectProvider objectProvider)
        {
            _spritesPathBindingData ??= SpritesBindingPath.ToPropertyBindingData();
            _spritesProperty = objectProvider.RentReadOnlyProperty<IList<Sprite>>(context, _spritesPathBindingData);

            StartAnimating();
        }

        public void ResetBindingContext(IObjectProvider objectProvider)
        {
            if (_spritesProperty == null)
                return;

            objectProvider.ReturnReadOnlyProperty(_spritesProperty);
            _spritesProperty = null;
            
            StopAnimating();
        }

        private void StartAnimating()
        {
            _currentFrame = 0;
            _animationToken = new();
            Animate(_animationToken.Token);
        }

        private async void Animate(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                if (++_currentFrame >= Sprites.Count)
                {
                    _currentFrame = 0;

                    await DOTween.ToAlpha(
                        () => style.unityBackgroundImageTintColor.value,
                        value => style.unityBackgroundImageTintColor = value,
                        0f,
                        FadeDuration)
                        .SetEase(Ease.OutSine)
                        .OnUpdate(()=> UnityEngine.Debug.Log(style.unityBackgroundImageTintColor.value))
                        .Play().ToUniTask(cancellationToken: token);

                }
                else
                {
                    style.unityBackgroundImageTintColor = Color.white;
                    SetImage(Sprites[_currentFrame]);
                }
                
                await Task.Delay(FrameWaitTime, token);
            }
        }

        private void StopAnimating()
        {
            _animationToken.Cancel();
            _animationToken.Dispose();
        }

        public new class UxmlTraits : Image.UxmlTraits
        {
            private readonly UxmlIntAttributeDescription _framesPerSecond = new()
                { name = "frames-per-second", defaultValue = 30 };

            private readonly UxmlFloatAttributeDescription _fadeDuration = new()
                { name = "fade-duration", defaultValue = 0.25f };

            private readonly UxmlStringAttributeDescription _spritesBindingPath = new()
                { name = "sprites-binding-path", defaultValue = "" };

            public override void Init(VisualElement visualElement, IUxmlAttributes bag, CreationContext context)
            {
                base.Init(visualElement, bag, context);
                var view = (LoadingIconView)visualElement;
                
                view.FramesPerSecond = _framesPerSecond.GetValueFromBag(bag, context);
                view.FadeDuration = _fadeDuration.GetValueFromBag(bag, context);
                view.SpritesBindingPath = _spritesBindingPath.GetValueFromBag(bag, context);
            }
        }

        public new class UxmlFactory : UxmlFactory<LoadingIconView, UxmlTraits> { }

    }
}