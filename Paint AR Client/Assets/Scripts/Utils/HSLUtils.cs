using UnityEngine;

public static class HSLUtils
{
    public static HSL ToHSL(this Color rgb)
    {
        var hsl = new HSL
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

    public struct HSL
    {
        public int H { get; set; }
        public float S { get; set; }
        public float L { get; set; }
        public float A { get; set; }

        public HSL(int h, float s, float l, float a = 1f)
        {
            H = h;
            S = s;
            L = l;
            A = a;
        }

        public Color ToRGB() => HSLToRGB(this);

        private static Color HSLToRGB(HSL hsl)
        {
            var r = 0f;
            var g = 0f;
            var b = 0f;

            if (hsl.S == 0)
            {
                r = g = b = hsl.L;
            }
            else
            {
                var hue = (float)hsl.H / 360;

                var v2 = hsl.L < 0.5 ? hsl.L * (1 + hsl.S) : hsl.L + hsl.S - hsl.L * hsl.S;
                var v1 = 2 * hsl.L - v2;

                r = HueToRGB(v1, v2, hue + 1.0f / 3);
                g = HueToRGB(v1, v2, hue);
                b = HueToRGB(v1, v2, hue - 1.0f / 3);
            }

            return new Color(r, g, b, hsl.A);
        }

        private static float HueToRGB(float v1, float v2, float vH)
        {
            if (vH < 0)
                vH += 1;

            if (vH > 1)
                vH -= 1;

            if (6 * vH < 1)
                return v1 + (v2 - v1) * 6 * vH;

            if (2 * vH < 1)
                return v2;

            if (3 * vH < 2)
                return v1 + (v2 - v1) * (2.0f / 3 - vH) * 6;

            return v1;
        }
    }
}