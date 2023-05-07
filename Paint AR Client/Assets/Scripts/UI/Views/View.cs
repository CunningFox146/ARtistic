using System;
using ArPaint.UI.Systems.Stack;
using ArPaint.UI.ViewModels;
using UnityMvvmToolkit.UITK;
using Zenject;

namespace ArPaint.UI.Views
{
    public abstract class View<TViewModel> : DocumentView<TViewModel>, IStackableView where TViewModel : ViewModel
    {
        public event Action OnDestroy;
        public event Action OnHide;
        public event Action OnShow;

        private TViewModel _viewModel;

        public void Show()
        {
            OnShow?.Invoke();
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            OnHide?.Invoke();
            gameObject.SetActive(false);
        }

        public void Destroy()
        {
            OnDestroy?.Invoke();
            Destroy(gameObject);
        }

        [Inject]
        private void Constructor(TViewModel injectedViewModel)
        {
            _viewModel = injectedViewModel;
        }

        protected override TViewModel GetBindingContext()
        {
            return _viewModel;
        }
    }
}