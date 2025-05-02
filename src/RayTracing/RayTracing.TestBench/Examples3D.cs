using RayTracing.Base;
using RayTracing.Math;
using RayTracing.Targets;
using System.Drawing;

namespace RayTracing.TestBench
{
    internal static class Examples3D
    {
        public static void DrawTriangles(IRenderTarget target)
        {
            Argument.AssertNotNull(target, nameof(target));

            var points = new Vector3[]
            {
                new Vector3 (194, 298, 294),
                new Vector3 (38, 182, 141),
                new Vector3 (152, 34, 212),

                new Vector3 (194, 298, 294),
                new Vector3 (152, 34, 212),
                new Vector3 (206, 150, 366),

                new Vector3 (362, 218, 259),
                new Vector3 (206, 150, 366),
                new Vector3 (152, 34, 212),

                new Vector3 (362, 218, 259),
                new Vector3 (152, 34, 212),
                new Vector3 (307, 102, 106),

                new Vector3 (307, 102, 106),
                new Vector3 (152, 34, 212),
                new Vector3 (38, 182, 141),

                new Vector3 (307, 102, 106),
                new Vector3 (38, 182, 141),
                new Vector3 (194, 250, 34),
            };

            for (var i = 0; i < points.Length / 3; i++)
            {
                var triangle = new Triangle3D(
                    points[i],
                    points[i + 1],
                    points[i + 2]);

                DrawTriangle(target, triangle);
            }
        }

        private static void DrawTriangle(IRenderTarget target, Triangle3D triangle)
        {
            Argument.AssertNotNull(target, nameof(target));
            Argument.AssertNotNull(triangle, nameof(triangle));

            DrawLine(target, triangle.CornerA, triangle.CornerB);
            DrawLine(target, triangle.CornerB, triangle.CornerC);
            DrawLine(target, triangle.CornerC, triangle.CornerA);
        }

        private static void DrawLine(IRenderTarget target, Vector3 a, Vector3 b)
        {
            Argument.AssertNotNull(target, nameof(target));

            var p = a;
            var u = b - a;

            var steps = 50;
            var stepFactor = 1.0f / steps;

            for (var i = 0; i < steps; i++)
            {
                var lambda = i * stepFactor;
                var r = p + (lambda * u);

                target.Pixel((int)r.X, (int)r.Y, Color.BlueViolet);
            }
        }
    }
}
