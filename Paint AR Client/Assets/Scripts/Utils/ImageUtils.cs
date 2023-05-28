using System.Threading;
using Cysharp.Threading.Tasks;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Rendering;

namespace ArPaint.Utils
{
    public static class ImageUtils
    {
        public static async UniTask<byte[]> ToBytesArray(this Texture sourceTexture, uint? widthOverride = null,
            uint? heightOverride = null,
            CancellationToken cancellationToken = default)
        {
            var width = widthOverride ?? (uint)sourceTexture.width;
            var height = heightOverride ?? (uint)sourceTexture.height;
            
            var resizeTexture = RenderTexture.GetTemporary((int)width, (int)height, 0);
            Graphics.Blit(sourceTexture, resizeTexture);
            
            var nativeArray = new NativeArray<byte>(resizeTexture.width * resizeTexture.height * 16, Allocator.Persistent,
                NativeArrayOptions.UninitializedMemory);

            var request = AsyncGPUReadback.RequestIntoNativeArray(ref nativeArray, resizeTexture);

            await request.ToUniTask(cancellationToken: cancellationToken);

            var bytes = default(byte[]);
            if (!request.hasError)
            {
                var pngArray = ImageConversion.EncodeNativeArrayToPNG(nativeArray, resizeTexture.graphicsFormat,
                    width, height);

                bytes = pngArray.ToArray();
                pngArray.Dispose();
            }

            nativeArray.Dispose();

            return bytes;
        }
    }
}