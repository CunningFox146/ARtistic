using ArPaint.UI.Services.Stack;
using UnityMvvmToolkit.Core.Interfaces;

namespace ArPaint.UI.ViewModels
{
    public abstract class ViewModel : IBindingContext
    {
        public IViewStack ViewStack { get; set; }
    }
}