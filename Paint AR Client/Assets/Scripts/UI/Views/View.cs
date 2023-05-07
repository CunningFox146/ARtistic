using System;
using ArPaint.UI.Systems.Stack;
using ArPaint.UI.ViewModels;
using UnityMvvmToolkit.UITK;
using Zenject;

namespace ArPaint.UI.Views
{
    public abstract class View<TViewModel> : DocumentView<TViewModel>, IStackableView where TViewModel : ViewModel
    {
        private TViewModel _viewModel;

        public virtual void Show()
        {
            RootVisualElement.SendEvent(new ViewShownEvent());
            RootVisualElement.visible = false;
        }

        public virtual void Hide()
        {
            RootVisualElement.SendEvent(new ViewHiddenEvent());
            RootVisualElement.visible = false;
        }

        public virtual void Destroy()
        {
            Hide();
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