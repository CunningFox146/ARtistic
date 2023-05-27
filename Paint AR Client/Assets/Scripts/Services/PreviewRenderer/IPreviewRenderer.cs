using System;
using System.Collections.Generic;
using ArPaint.Services.Commands;
using UnityEngine;

namespace Services.PreviewRenderer
{
    public interface IPreviewRenderer
    {
        void RenderDrawing(IEnumerable<SerializableDrawCommand> commands);
        void SetRotationGetter(Func<Quaternion> rotation);
        void Clear();
    }
}