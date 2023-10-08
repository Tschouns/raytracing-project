using RayTracing.Base;
using RayTracing.CanvasClient;
using RayTracing.Math;
using RayTracing.Rendering;

namespace RayTracing.TestBench
{
    public static class ExamplesRendering
    {
        public static void Camera(ICanvas canvas)
        {
            Argument.AssertNotNull(canvas, nameof(canvas));

            // Create a "model".
            var v1 = new Vector3(1, 0, -1);
            var v2 = new Vector3(3, 0, -2);
            var v3 = new Vector3(4, 0, 0);
            var v4 = new Vector3(2, 0, 1);
            var v5 = new Vector3(1, 2, -1);
            var v6 = new Vector3(3, 2, -2);
            var v7 = new Vector3(4, 2, 0);
            var v8 = new Vector3(2, 2, 1);

            var triables = new List<Triangle3D>
            {
                new Triangle3D(v1, v2, v5),
                new Triangle3D(v2, v3, v6),
                new Triangle3D(v3, v4, v7),
                new Triangle3D(v4, v1, v8),
            };

            ushort resX = 1200;
            ushort resY = 1200;

            // Setup camera.
            var camera = new Camera(resX, resY);
            camera.Position = new Vector3(4, 5, -15);
            camera.LookingDirection = new Vector3(-1, -2, 7);
            camera.FocalLength *= 2;

            // Setup canvas.
            canvas.Size(resX, resY);

            // Render image.
            var canvasTarget = new CanvasRenderTarget(canvas);
            var renderer = new SimpleRenderer();
            renderer.Render(triables, camera, canvasTarget);
        }
    }
}
