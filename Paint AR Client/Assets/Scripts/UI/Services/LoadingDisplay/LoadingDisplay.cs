using System;
using ArPaint.UI.Views.Loading;
using Object = UnityEngine.Object;

namespace ArPaint.UI.Services.LoadingDisplay
{
    public class LoadingDisplay : ILoadingDisplay, IDisposable
    {
        private readonly LoadingView _loadingView;

        public LoadingDisplay(LoadingView.Factory loadingViewFactory)
        {
            _loadingView = loadingViewFactory.Create();
            Object.DontDestroyOnLoad(_loadingView.gameObject);
        }

        public void ShowLoadingView()
        {
            _loadingView.Show();
        }

        public void HideLoadingView()
        {
            _loadingView.Hide();
        }

        public void Dispose()
        {
            if (_loadingView)
                _loadingView.Destroy();
        }
    }
}