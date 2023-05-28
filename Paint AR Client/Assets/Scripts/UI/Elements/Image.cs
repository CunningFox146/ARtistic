﻿using UnityEngine;
using UnityEngine.UIElements;

namespace ArPaint.UI.Elements
{
    public class Image : VisualElement
    {
        public void SetImage(Sprite image)
        {
            style.backgroundImage = new StyleBackground(image);
        }
        
        public void SetImage(Texture2D image)
        {
            style.backgroundImage = new StyleBackground(image);
        }
        
        public void SetImage(VectorImage image)
        {
            style.backgroundImage = new StyleBackground(image);
        }

        public new class UxmlFactory : UxmlFactory<Image, UxmlTraits> {}
    
        public new class UxmlTraits : VisualElement.UxmlTraits {}
    }
}