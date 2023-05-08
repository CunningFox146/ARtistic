using UnityEngine;

namespace ArPaint.Utils.Hsl
{
    public static class HSLUtils
    {
        public static ColorHsl ToHSL(this Color rgb)
        {
            var hsl = new ColorHsl
            {
                A = rgb.a
            };

            var r = rgb.r;
            var g = rgb.g;
            var b = rgb.b;

            var min = Mathf.Min(Mathf.Min(r, g), b);
            var max = Mathf.Max(Mathf.Max(r, g), b);
            var delta = max - min;

            hsl.L = (max + min) / 2;

            if (delta == 0)
            {
                hsl.H = 0;
                hsl.S = 0.0f;
            }
            else
            {
                hsl.S = hsl.L <= 0.5 ? delta / (max + min) : delta / (2 - max - min);

                float hue;

                if (Mathf.Approximately(r, max))
                    hue = (g - b) / 6 / delta;
                else if (Mathf.Approximately(g, max))
                    hue = 1.0f / 3 + (b - r) / 6 / delta;
                else
                    hue = 2.0f / 3 + (r - g) / 6 / delta;

                if (hue < 0)
                    hue += 1;
                if (hue > 1)
                    hue -= 1;

                hsl.H = (int)(hue * 360);
            }

            return hsl;
        }

        
    }
}