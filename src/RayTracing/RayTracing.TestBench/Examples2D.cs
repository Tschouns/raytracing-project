
using RayTracing.Base;
using RayTracing.Math;
using RayTracing.Math.Calculations;
using RayTracing.Targets;
using System.Drawing;

namespace RayTracing.TestBench
{
    internal static class Examples2D
    {
        public static void DrawLine(IRenderTarget target)
        {
            Argument.AssertNotNull(target, nameof(target));

            var p = new Vector2(10, 10);
            var u = new Vector2(2, 1);

            for (var lambda = 0; lambda < 30; lambda++)
            {
                var r = p + (lambda * u);
                target.Pixel((int)r.X, (int)r.Y, Color.Orange);
            }
        }

        public static void DrawIntersectingLines(IRenderTarget target)
        {
            Argument.AssertNotNull(target, nameof(target));

            var p = new Vector2(10, 10);
            var u = new Vector2(2, 1);

            var q = new Vector2(20, 70);
            var v = new Vector2(1, -2);

            for (var lambda = 0; lambda < 30; lambda++)
            {
                var r = p + (lambda * u);
                target.Pixel((int)r.X, (int)r.Y, Color.BlueViolet);
            }

            for (var lambda = 0; lambda < 30; lambda++)
            {
                var r = q + (lambda * v);
                target.Pixel((int)r.X, (int)r.Y, Color.GreenYellow);
            }

            var i = VectorCalculator2D.Intersect(p, u, q, v)!;
            target.Pixel((int)i.Value.X, (int)i.Value.Y, Color.Red);
        }
    }
}
