
using RayTracing.Base;
using RayTracing.CanvasClient;
using RayTracing.Math;
using System.Drawing;

namespace RayTracing.TestBench
{
    internal static class Examples
    {
        public static void DrawLine(ICanvas canvas)
        {
            Argument.AssertNotNull(canvas, nameof(canvas));

            Vector2 p = new Vector2(10, 10);
            Vector2 u = new Vector2(2, 1);

            canvas.Size(100, 100);

            for (int lambda = 0; lambda < 30; lambda++)
            {
                var r = p + lambda * u;
                canvas.Pixel((int)r.X, (int)r.Y, Color.Orange);
            }
        }
    }
}
