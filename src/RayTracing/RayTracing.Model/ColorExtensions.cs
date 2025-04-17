using System.Drawing;

namespace RayTracing.Model
{
    public static class ColorExtensions
    {
        public static ArgbColor ToArgbColor(this Color color)
        {
            return new ArgbColor(
                color.R / 255f,
                color.G / 255f,
                color.B / 255f,
                color.A / 255f);
        }
    }
}
