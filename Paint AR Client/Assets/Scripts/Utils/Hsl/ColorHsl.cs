using UnityEngine;

namespace ArPaint.Utils.Hsl
{
    public struct ColorHsl
    {
        public int H { get; set; }
        public float S { get; set; }
        public float L { get; set; }
        public float A { get; set; }
    
        public ColorHsl(int h, float s, float l, float a = 1f)
        {
            H = h;
            S = s;
            L = l;
            A = a;
        }
    
        public Color ToRGB() => HSLToRGB(this);
        public ColorHsl SetH(int h) => new ColorHsl(h, S, L, A);
        public ColorHsl SetS(float s) => new ColorHsl(H, s, L, A);
        public ColorHsl SetL(float l) => new ColorHsl(H, S, l, A);
        public ColorHsl SetA(float a) => new ColorHsl(H, S, L, a);
    
        private static Color HSLToRGB(ColorHsl colorHsl)
        {
            var r = 0f;
            var g = 0f;
            var b = 0f;
    
            if (colorHsl.S == 0)
            {
                r = g = b = colorHsl.L;
            }
            else
            {
                var hue = (float)colorHsl.H / 360;
    
                var v2 = colorHsl.L < 0.5 ? colorHsl.L * (1 + colorHsl.S) : colorHsl.L + colorHsl.S - colorHsl.L * colorHsl.S;
                var v1 = 2 * colorHsl.L - v2;
    
                r = HueToRGB(v1, v2, hue + 1.0f / 3);
                g = HueToRGB(v1, v2, hue);
                b = HueToRGB(v1, v2, hue - 1.0f / 3);
            }
    
            return new Color(r, g, b, colorHsl.A);
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
        
        public override string ToString()
            => $"H: {H} S: {S} L: {L} A: {A} (RGB: {ToRGB()})";
    }
}