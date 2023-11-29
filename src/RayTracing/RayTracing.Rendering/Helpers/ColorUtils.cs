

using System.Drawing;

namespace RayTracing.Rendering.Helpers
{
    internal static class ColorUtils
    {
        private static float COLOR_MULTIPLICATION_FACTOR = 1 / 255f;

        public static Color Add(Color colorA, Color colorB)
        {
            return Color.FromArgb(
                System.Math.Min(colorA.R + colorB.R, 255),
                System.Math.Min(colorA.G + colorB.G, 255),
                System.Math.Min(colorA.B + colorB.B, 255));
        }

        public static Color Multiply(Color colorA, Color colorB)
        {
            return Color.FromArgb(
                MultiplyColorValues(colorA.R, colorB.R),
                MultiplyColorValues(colorA.G, colorB.G),
                MultiplyColorValues(colorA.B, colorB.B));
        }

        public static Color Scale(Color color, float scale)
        {
            return Color.FromArgb(
                ScaleColorValue(color.R, scale),
                ScaleColorValue(color.G, scale),
                ScaleColorValue(color.B, scale));
        }

        private static byte MultiplyColorValues(byte valueA, byte valueB)
        {
            var newValue = valueA * valueB * COLOR_MULTIPLICATION_FACTOR;

            return Convert.ToByte(newValue);
        }

        private static byte ScaleColorValue(byte value, float scale)
        {
            var scaledValue = value * scale;
            var clampedValue = System.Math.Clamp(scaledValue, 0, 255);

            return Convert.ToByte(clampedValue);
        }
    }
}
