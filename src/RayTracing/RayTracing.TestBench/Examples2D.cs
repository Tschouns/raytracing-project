
using RayTracing.Base;
using RayTracing.CanvasClient;
using RayTracing.Math;
using RayTracing.Math.Calculations;
using System.Drawing;

namespace RayTracing.TestBench
{
    internal static class Examples2D
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

        public static void DrawIntersectingLines(ICanvas canvas)
        {
            Argument.AssertNotNull(canvas, nameof(canvas));

            var p = new Vector2(10, 10);
            var u = new Vector2(2, 1);

            var q = new Vector2(20, 70);
            var v = new Vector2(1, -2);

            canvas.Size(100, 100);

            for (int lambda = 0; lambda < 30; lambda++)
            {
                var r = p + lambda * u;
                canvas.Pixel((int)r.X, (int)r.Y, Color.BlueViolet);
            }

            for (int lambda = 0; lambda < 30; lambda++)
            {
                var r = q + lambda * v;
                canvas.Pixel((int)r.X, (int)r.Y, Color.GreenYellow);
            }

            var i = VectorCalculator2D.Intersect(p, u, q, v)!;
            canvas.Pixel((int)i.Value.X, (int)i.Value.Y, Color.Red);
        }
    }
}
