using RayTracing.Base;
using RayTracing.Gui;
using RayTracing.Rendering.Targets;
using System.Drawing;

namespace RayTracing.TestBench
{
    public class CanvasWindowRenderTarget : IRenderTarget
    {
        private readonly ICanvas canvas;

        public CanvasWindowRenderTarget(ICanvas canvas)
        {
            Argument.AssertNotNull(canvas, nameof(canvas));

            this.canvas = canvas;
        }

        public void Fill(Color color)
        {
            canvas.Fill(color);
        }

        public void SetPixel(int x, int y, Color color)
        {
            canvas.Pixel(x, y, color);
        }
    }
}
