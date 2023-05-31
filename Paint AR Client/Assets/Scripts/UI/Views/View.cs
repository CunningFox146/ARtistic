using ArPaint.UI.Services.Stack;
using ArPaint.UI.ViewModels;
using UnityEngine.UIElements;
using UnityMvvmToolkit.Core.Interfaces;
using UnityMvvmToolkit.UITK;
using Zenject;

namespace ArPaint.UI.Views
{
    public abstract class View<TViewModel> : DocumentView<TViewModel>, IStackableView, ISortableView
        where TViewModel : ViewModel
    {
        private UIDocument _document;
        private IValueConverter[] _valueConverters;
        private TViewModel _viewModel;

        public void SetSortOrder(int sortOrder)
        {
            _document.sortingOrder = sortOrder;
        }

        public virtual void Show()
        {
            if (RootVisualElement == null || RootVisualElement.visible)
                return;

            RootVisualElement.visible = true;

            // TODO: Refactor
            RootVisualElement.Query<VisualElement>().Where(element => element is IViewShownHandler).ForEach(
                element => { ((IViewShownHandler)element).OnViewShown(this); });

            (_viewModel as INotifyViewActive)?.OnViewActive();
        }

        public virtual void Hide()
        {
            if (RootVisualElement is not { visible: true })
                return;

            // TODO: Refactor
            RootVisualElement.Query<VisualElement>().Where(element => element is IViewHiddenHandler).ForEach(
                element => { ((IViewHiddenHandler)element).OnViewHidden(this); });
            RootVisualElement.visible = false;
            
            (_viewModel as INotifyViewInactive)?.OnViewInactive();
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

        protected override void OnInit()
        {
            base.OnInit();
            _document = GetComponent<UIDocument>();
        }

        [Inject]
        private void Constructor(TViewModel injectedViewModel, IValueConverter[] valueConverters)
        {
            _viewModel = injectedViewModel;
            _valueConverters = valueConverters;
        }

        protected override TViewModel GetBindingContext()
            => _viewModel;

        protected override IValueConverter[] GetValueConverters()
            => _valueConverters;
    }
}