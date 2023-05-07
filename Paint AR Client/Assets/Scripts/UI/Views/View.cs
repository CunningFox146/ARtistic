using System;
using ArPaint.UI.Systems;
using ArPaint.UI.Systems.Stack;
using ArPaint.UI.ViewModels;
using ArPaint.UI.ViewModels.Draw;
using UnityMvvmToolkit.UITK;
using Zenject;

namespace ArPaint.UI.Views
{
    public abstract class View<TViewModel> : DocumentView<TViewModel>, IStackableView where TViewModel : ViewModel
    {
        public event Action OnDestroy;
        public event Action OnHide;
        public event Action OnShow;

        protected TViewModel viewModel;
        
        [Inject]
        protected void Constructor(TViewModel injectedViewModel)
        {
            viewModel = injectedViewModel;
        }
        
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

        protected override TViewModel GetBindingContext()
        {
            return viewModel;
        }
    }
}