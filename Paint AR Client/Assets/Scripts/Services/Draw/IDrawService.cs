﻿using ArPaint.Services.Draw.Brushes;
using ArPaint.Services.Draw.Shapes;

namespace ArPaint.Services.Draw
{
    public interface IDrawService
    {
        IShape Shape { get; set; }
        Brush Brush { get; set; }
        void Save();
    }
}