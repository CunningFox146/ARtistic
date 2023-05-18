using System;
using System.Threading;
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
            
            var width = texture.width;
            var height = texture.height;
            var nativeArray = new NativeArray<byte>(width * height * 16, Allocator.Persistent,
                NativeArrayOptions.UninitializedMemory);

            var request = AsyncGPUReadback.RequestIntoNativeArray(ref nativeArray, texture, 0);

            await request.ToUniTask(cancellationToken: cancellationToken);
            
            if (!request.hasError)
            {
                var encoded = ImageConversion.EncodeNativeArrayToPNG(nativeArray, texture.graphicsFormat,
                    (uint)width, (uint)height);

                NativeGallery.SaveImageToGallery(encoded.ToArray(), "ARtistic",
                    $"Drawing {DateTime.Now.ToLongDateString()}.png",
                    (success, path) =>
                    {
                        _toast.ShowMessage(!success ? "Failed to save. Check permissions." : $"Saved to {path}");
                    });
                encoded.Dispose();
            }

            nativeArray.Dispose();
        }
    }
}