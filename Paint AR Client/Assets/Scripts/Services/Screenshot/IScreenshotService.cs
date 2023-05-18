using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Services.Screenshot
{
    public interface IScreenshotService
    {
        UniTask Screenshot(RenderTexture texture, CancellationToken cancellationToken = default);
    }
}