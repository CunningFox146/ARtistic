using ArPaint.UI.ViewModels.ArInit;
using DG.Tweening;
using UnityEngine.UIElements;
using Zenject;

namespace ArPaint.UI.Views.ArInit
{
    public class ArInitView : View<ArInitViewModel>
    {
        private const string PhoneClassName = "ar-anim__phone";
        private const string BgClassName = "ar_anim__circle_container";
        
        private VisualElement _phone;
        private VisualElement _bg;
        private Sequence _anim;
        
        protected override void OnInit()
        {
            base.OnInit();
            
            _phone = RootVisualElement.Q<VisualElement>(className: PhoneClassName);
            _bg = RootVisualElement.Q<VisualElement>(className: BgClassName);
        }

        private void StartAnimation()
        {
            const float angleDelta = 25f;
            const float phoneRotateDuration = 1.25f;
            const float phoneMoveDuration = 2f;
            var phoneOffset = 100f;
            var bgOffset = 150f;
            
            _phone.style.rotate = new Rotate(-angleDelta);

            _anim = DOTween.Sequence()
                .Join(DOTween.To(
                        () => _phone.style.rotate.value.angle.ToDegrees(),
                        value => _phone.style.rotate = new Rotate(value),
                        angleDelta,
                        phoneRotateDuration)
                    .SetLoops(-1, LoopType.Yoyo)
                    .SetEase(Ease.InOutSine)
                    .Play())
                .Join(DOTween.To(
                        () => phoneOffset,
                        value =>
                        {
                            phoneOffset = value;
                            _phone.style.translate = new Translate(0f, phoneOffset);
                        },
                        -150f,
                        phoneMoveDuration)
                    .SetLoops(-1, LoopType.Yoyo)
                    .SetEase(Ease.InOutSine)
                    .Play());
        }

        private void StopAnimation()
        {
            _anim?.Kill();
            _anim = null;
        }

        public override void Show()
        {
            StartAnimation();
            base.Show();
        }

        public override void Hide()
        {
            StopAnimation();
            base.Hide();
        }

        public class Factory : PlaceholderFactory<ArInitView>{}
    }
}