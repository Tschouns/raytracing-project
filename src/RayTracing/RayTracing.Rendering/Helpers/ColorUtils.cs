

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

        private static byte MultiplyColorValues(byte valueA, byte valueB)
        {
            var newValue = valueA * valueB * COLOR_MULTIPLICATION_FACTOR;

            return Convert.ToByte(newValue);
        }
    }
}
