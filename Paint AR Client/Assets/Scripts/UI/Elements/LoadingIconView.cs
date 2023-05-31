﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ArPaint.UI.Services.Stack;
using ArPaint.UI.Views;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

namespace ArPaint.UI.Elements
{
    public class LoadingIconView : Image, IViewHiddenHandler, IViewShownHandler 
    {
        private CancellationTokenSource _animationToken;
        private int _currentFrame;
        private IList<Sprite> _sprites;

        public int FramesPerSecond { get; set; }
        public float FadeDuration { get; set; }
        private int FrameWaitTime => 1000 / FramesPerSecond;

        public LoadingIconView()
        {
            RegisterCallback<DetachFromPanelEvent>(_ => StopAnimating());
        }
        
        public void OnViewHidden(IStackableView view)
        {
            StopAnimating();
        }

        public void OnViewShown(IStackableView view)
        {
            StartAnimating();
        }

        public void SetSprites(IList<Sprite> sprites)
        {
            _sprites = sprites;
            StartAnimating();
        }

        private void StartAnimating()
        {
            if (_animationToken != null || !_sprites.Any())
                return;
            
            _currentFrame = 0;
            _animationToken = new CancellationTokenSource();
            Animate(_animationToken.Token);
        }

        private void StopAnimating()
        {
            if (_animationToken == null)
                return;
            
            _animationToken.Cancel();
            _animationToken.Dispose();
            _animationToken = null;
        }

        private async void Animate(CancellationToken token)
        {
            try
            {
                while (!token.IsCancellationRequested)
                {
                    if (++_currentFrame >= _sprites.Count)
                    {
                        _currentFrame = 0;

                        await DOTween.To(
                                () => style.opacity.value,
                                value => style.opacity = value,
                                0f,
                                FadeDuration)
                            .SetEase(Ease.OutSine)
                            .Play().ToUniTask(cancellationToken: token);
                    }
                    else
                    {
                        style.opacity = 1f;
                        SetImage(_sprites[_currentFrame]);
                    }

                    await Task.Delay(FrameWaitTime, token);
                }
            }
            catch (TaskCanceledException) { }
        }

        public new class UxmlTraits : Image.UxmlTraits
        {
            private readonly UxmlFloatAttributeDescription _fadeDuration = new()
                { name = "fade-duration", defaultValue = 0.25f };

            private readonly UxmlIntAttributeDescription _framesPerSecond = new()
                { name = "frames-per-second", defaultValue = 30 };

            public override void Init(VisualElement visualElement, IUxmlAttributes bag, CreationContext context)
            {
                base.Init(visualElement, bag, context);
                var view = (LoadingIconView)visualElement;

                view.FramesPerSecond = _framesPerSecond.GetValueFromBag(bag, context);
                view.FadeDuration = _fadeDuration.GetValueFromBag(bag, context);
            }
        }

        public new class UxmlFactory : UxmlFactory<LoadingIconView, UxmlTraits>
        {
        }

    }
}