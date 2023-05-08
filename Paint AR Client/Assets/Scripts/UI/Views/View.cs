using System;
using ArPaint.UI.Systems.Stack;
using ArPaint.UI.ViewModels;
using UnityEngine.UIElements;
using UnityMvvmToolkit.UITK;
using Zenject;

namespace ArPaint.UI.Views
{
    public abstract class View<TViewModel> : DocumentView<TViewModel>, IStackableView where TViewModel : ViewModel
    {
        private TViewModel _viewModel;

        public virtual void Show()
        {
            RootVisualElement.visible = true;
            RootVisualElement.Query<VisualElement>().Where(element => element is IViewShownHandler).ForEach(
                element => { ((IViewShownHandler)element).OnViewShown(this); });   
        }

        public virtual void Hide()
        {
            RootVisualElement.Query<VisualElement>().Where(element => element is IViewHiddenHandler).ForEach(
                element => { ((IViewHiddenHandler)element).OnViewHidden(this); });
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