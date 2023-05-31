﻿namespace ArPaint.UI.Services.Stack
{
    public interface IStackableView
    {
        public void Show();
        public void Hide();
        public void Destroy();
        public void SetViewStack(IViewStack viewStack);
    }
}