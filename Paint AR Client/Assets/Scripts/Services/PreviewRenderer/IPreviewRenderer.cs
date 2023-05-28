using System;
using System.Collections.Generic;
using ArPaint.Services.Commands;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Services.PreviewRenderer
{
    public interface IPreviewRenderer
    {
        public RenderTexture RenderTexture { get; }
        UniTask RenderDrawing(IEnumerable<SerializableDrawCommand> commands);
        void SetRotationGetter(Func<Quaternion> rotation);
        void Clear();
    }
}