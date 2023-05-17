using System;
using ArPaint.UI.Systems.Stack;
using ArPaint.UI.ViewModels;
using UnityEngine.UIElements;
using UnityMvvmToolkit.UITK;
using Zenject;

namespace ArPaint.UI.Views
{
    public abstract class View<TViewModel> : DocumentView<TViewModel>, IStackableView, ISortableView
        where TViewModel : ViewModel
    {
        private TViewModel _viewModel;
        private UIDocument _document;

        protected override void OnInit()
        {
            base.OnInit();
            _document = GetComponent<UIDocument>();
        }

        public virtual void Show()
        {
            if (RootVisualElement == null)
                return;
            
            gameObject.SetActive(true);
            RootVisualElement.visible = true;
            
            // TODO: Refactor
            RootVisualElement.Query<VisualElement>().Where(element => element is IViewShownHandler).ForEach(
                element => { ((IViewShownHandler)element).OnViewShown(this); });   
        }

        public virtual void Hide()
        {
            if (RootVisualElement == null)
            {
                gameObject.SetActive(false);
                return;
            }
            // TODO: Refactor
            RootVisualElement.Query<VisualElement>().Where(element => element is IViewHiddenHandler).ForEach(
                element => { ((IViewHiddenHandler)element).OnViewHidden(this); });
            RootVisualElement.visible = false;
        }

        public virtual void Destroy()
        {
            Hide();
            Destroy(gameObject);
        }

        public void SetViewStack(IViewStack viewStack)
        {
            _viewModel.ViewStack = viewStack;
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

        public void SetSortOrder(int sortOrder)
        {
            _document.sortingOrder = sortOrder;
        }
    }
}