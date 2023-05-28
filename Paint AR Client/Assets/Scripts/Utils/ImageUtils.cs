using System.Threading;
using Cysharp.Threading.Tasks;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Rendering;

namespace ArPaint.Utils
{
    public static class ImageUtils
    {
        public static Texture2D ToTexture2D(this byte[] bytes)
        {
            var texture = new Texture2D(1, 1);
            texture.LoadImage(bytes);
            return texture;
        }

        public static async UniTask<byte[]> ToBytesArray(this Texture source,
            int width = 0,
            int height = 0,
            CancellationToken cancellationToken = default)
        {
            if (source is not (Texture2D or RenderTexture)) return null;

            if (width <= 0 || height <= 0)
            {
                width = source.width;
                height = source.height;
            }

            var resizeRT = RenderTexture.GetTemporary(width, height, 0);
            Graphics.Blit(source, resizeRT);

            var output = new NativeArray<byte>(width * height * 16, Allocator.Persistent,
                NativeArrayOptions.UninitializedMemory);

            var request = await AsyncGPUReadback.RequestIntoNativeArray(ref output, resizeRT)
                .ToUniTask(cancellationToken: cancellationToken);
            if (!request.hasError)
            {
                var encoded =
                    ImageConversion.EncodeNativeArrayToPNG(output, resizeRT.graphicsFormat, (uint)width, (uint)height);
                var result = encoded.ToArray();
                encoded.Dispose();
                return result;
            }

            output.Dispose();
            return null;
        }
    }
}