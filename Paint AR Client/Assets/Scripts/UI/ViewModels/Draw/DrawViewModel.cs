using UnityMvvmToolkit.Core;

namespace ArPaint.UI.ViewModels.Draw
{
    public class DrawViewModel : ViewModel
    {
        private string _text;
        
        public string Text
        {
            get => _text;
            set => Set(ref _text, value);
        }

        public DrawViewModel()
        {
            Text = "wow works!";
        }
    }
}