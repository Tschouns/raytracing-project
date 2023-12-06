using RayTracing.Base;
using RayTracing.CanvasClient;
using RayTracing.Math;
using RayTracing.ModelFiles.ColladaFormat;
using RayTracing.Rendering;
using RayTracing.Rendering.Cameras;
using RayTracing.Rendering.Settings;

namespace RayTracing.TestBench
{
    public static class ExamplesRendering
    {
        public static void Camera(ICanvas canvas)
        {
            Argument.AssertNotNull(canvas, nameof(canvas));

            // Load a model.
            var parser = new ColladaFileParser();
            var scene = parser.LoadFromFile(@"..\..\..\..\..\..\models\dummy\dummy.dae");

            // Setup camera.
            ushort resX = 600;
            ushort resY = 450;

            var camera = new Camera(resX, resY);
            camera.Position = new Vector3(0, 3, -5f);
            camera.LookingDirection = new Vector3(0, -0.5f, 2);
            camera.FocalLength /= 2f;

            // Setup canvas.
            canvas.Size(resX, resY);

            // Render image.
            var canvasTarget = new CanvasRenderTarget(canvas);
            var renderer = new RayTracerRenderer();

            while (true)
            {
                var rotate = Matrix4x4.RotateY(-0.523599f / 2f);

                foreach (var g in scene.Geometries)
                {
                    g.Transform(rotate);
                }

                renderer.Render(scene, camera, canvasTarget, new RenderSettings
                {
                    //AmbientLightColor = Color.DarkSlateBlue,
                    //DepthCueingColor = Color.AliceBlue,
                    DepthCueingMaxDistance = 100,
                    MaxRecursionDepth = 3,
                });

                Console.ReadLine();
            }
        }
    }
}
