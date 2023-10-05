using RayTracing.Base;
using RayTracing.CanvasClient;
using RayTracing.Math;
using System.Drawing;

namespace RayTracing.TestBench
{
    internal static class Examples3D
    {
        public static void DrawTriangles(ICanvas canvas)
        {
            Argument.AssertNotNull(canvas, nameof(canvas));

            canvas.Size(300, 300);

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

            for (int i = 0; i < points.Length / 3; i++)
            {
                var triangle = new Triangle3D(
                    points[i],
                    points[i + 1],
                    points[i + 2]);

                DrawTriangle(canvas, triangle);
            }
        }

        private static void DrawTriangle(ICanvas canvas, Triangle3D triangle)
        {
            Argument.AssertNotNull(canvas, nameof(canvas));
            Argument.AssertNotNull(triangle, nameof(triangle));

            DrawLine(canvas, triangle.PointA, triangle.PointB);
            DrawLine(canvas, triangle.PointB, triangle.PointC);
            DrawLine(canvas, triangle.PointC, triangle.PointA);
        }

        private static void DrawLine(ICanvas canvas, Vector3 a, Vector3 b)
        {
            Argument.AssertNotNull(canvas, nameof(canvas));

            var p = a;
            var u = b - a;

            var steps = 50;
            var stepFactor = 1.0f / steps;

            for (int i = 0; i < steps; i++)
            {
                var lambda = i * stepFactor;
                var r = p + lambda * u;

                canvas.Pixel((int)r.X, (int)r.Y, Color.BlueViolet);
            }
        }
    }
}
