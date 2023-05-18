using System.Collections.Generic;
using ArPaint.Services.Commands;

namespace Services.PreviewRenderer
{
    public interface IPreviewRenderer
    {
        void RenderDrawing(IEnumerable<SerializableDrawCommand> commands);
        void Clear();
    }
}