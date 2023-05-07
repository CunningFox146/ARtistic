using System.Collections.Generic;
using ArPaint.UI.Elements;
using ArPaint.UI.ViewModels.Loading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace ArPaint.UI.Views.Loading
{
    public class LoadingView : View<LoadingViewModel>
    {
        [SerializeField] private float _fadeDuration = 0.5f;
        [SerializeField] private List<Sprite> _loadingIconSprites;

        protected override void OnInit()
        {
            base.OnInit();

            RootVisualElement.style.opacity = 1f;
            RootVisualElement.Q<LoadingIconView>().SetSprites(_loadingIconSprites);
        }

        public void HideImmediate()
        {
            base.Hide();
        }

        public override async void Hide()
        {
            await HideAnimation();
            base.Hide();
        }

        private UniTask HideAnimation()
        {
            var style = RootVisualElement.style;
            return DOTween.To(
                    () => style.opacity.value,
                    value => style.opacity = value,
                    0f,
                    _fadeDuration)
                .SetEase(Ease.OutSine)
                .Play().ToUniTask(cancellationToken: this.GetCancellationTokenOnDestroy());
        }

        public override void Show()
        {
            RootVisualElement.style.opacity = 1f;
            base.Show();
        }

        public class Factory : PlaceholderFactory<LoadingView>
        {
        }
    }
}