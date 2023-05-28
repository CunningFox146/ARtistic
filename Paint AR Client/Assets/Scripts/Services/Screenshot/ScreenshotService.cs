using System;
using System.Threading;
using ArPaint.Utils;
using Cysharp.Threading.Tasks;
using Services.Toast;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Rendering;

namespace Services.Screenshot
{
    public class ScreenshotService : IScreenshotService
    {
        private readonly IToast _toast;

        public ScreenshotService(IToast toast)
        {
            _toast = toast;
        }

        public async UniTask Screenshot(RenderTexture texture, CancellationToken cancellationToken = default)
        {
            NativeGallery.CheckPermission(NativeGallery.PermissionType.Write, NativeGallery.MediaType.Image);

            var bytes = await texture.ToBytesArray(cancellationToken: cancellationToken);

            if (bytes != null)
            {
                NativeGallery.SaveImageToGallery(bytes, "ARtistic",
                    $"Drawing {DateTime.Now.ToLongDateString()}.png",
                    (success, path) =>
                    {
                        _toast.ShowMessage(!success ? "Failed to save. Check permissions." : $"Saved to {path}");
                    });
            }
        }
    }
}