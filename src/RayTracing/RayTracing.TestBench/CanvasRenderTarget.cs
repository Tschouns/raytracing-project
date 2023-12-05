using RayTracing.Base;
using RayTracing.CanvasClient;
using RayTracing.Rendering.Targets;
using System.Drawing;

namespace RayTracing.TestBench
{
    /// <summary>
    /// Wraps an <see cref="ICanvas"/> as an <see cref="IRenderTarget"/>.
    /// </summary>
    internal class CanvasRenderTarget : IRenderTarget
    {
        private readonly ICanvas canvas;

        public CanvasRenderTarget(ICanvas canvas)
        {
            Argument.AssertNotNull(canvas, nameof(canvas));

            this.canvas = canvas;
        }

        public void Fill(Color color)
        {
            this.canvas.Fill(color);
        }

        public void SetPixel(int x, int y, Color color)
        {
            this.canvas.Pixel(x, y, color);
        }
    }
}
