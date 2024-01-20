
using System.Drawing;

namespace RayTracing.Gui
{
    public struct Pixel
    {
        public Pixel(int x, int y, Color color)
        {
            this.X = x;
            this.Y = y;
            this.Color = color;
        }

        public int X;
        public int Y;
        public Color Color;
    }
}
