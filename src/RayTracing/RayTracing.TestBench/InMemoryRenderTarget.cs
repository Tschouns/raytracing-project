using RayTracing.Base;
using RayTracing.Targets;
using System.Drawing;

namespace RayTracing.TestBench
{
    /// <summary>
    /// A render target implementation which simply stores pixels in-memory.
    /// It can later apply the stored image to another render target.
    /// </summary>
    public class InMemoryRenderTarget : IRenderTarget
    {
        private readonly ushort width;
        private readonly ushort height;
        private readonly Color[,] pixels;

        public InMemoryRenderTarget(ushort width, ushort height)
        {
            this.width = width;
            this.height = height;
            this.pixels = new Color[width, height];
        }

        public void Fill(Color color)
        {
            this.ForEachPixel((x, y) => Pixel(x, y, color));
        }

        public void Pixel(int x, int y, Color color)
        {
            this.pixels[x, y] = color;
        }

        public void ApplyImageToTarget(IRenderTarget target)
        {
            Argument.AssertNotNull(target, nameof(target));

            this.ForEachPixel((x, y) => target.Pixel(x, y, this.pixels[x, y]));
        }

        private void ForEachPixel(Action<ushort, ushort> pixelAction)
        {
            for (ushort x = 0; x < this.width; x++)
            {
                for (ushort y = 0; y < this.height; y++)
                {
                    pixelAction(x, y);
                }
            }
        }
    }
}
