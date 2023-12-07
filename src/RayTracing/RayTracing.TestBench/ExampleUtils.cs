using RayTracing.Base;
using RayTracing.CanvasClient;
using RayTracing.ModelFiles.ColladaFormat;
using RayTracing.Rendering.Cameras;
using RayTracing.Rendering.Settings;
using RayTracing.Rendering;
using System.Drawing;
using RayTracing.Math;
using RayTracing.Model;

namespace RayTracing.TestBench
{
    internal static class ExampleUtils
    {
        public static void RenderToCanvas(
            ICanvas canvas,
            string colladaFile,
            Action<Scene> configureScene,
            ushort resX,
            ushort resY,
            Action<ICameraSettings> configureCamera,
            IRenderSettings renderSettings)
        {
            Argument.AssertNotNull(canvas, nameof(canvas));
            Argument.AssertNotNull(colladaFile, nameof(colladaFile));
            Argument.AssertNotNull(configureScene, nameof(configureScene));
            Argument.AssertNotNull(configureCamera, nameof(configureCamera));
            Argument.AssertNotNull(renderSettings, nameof(renderSettings));

            // Load a model.
            var parser = new ColladaFileParser();
            var scene = parser.LoadFromFile(colladaFile);

            // Setup camera.
            var camera = new Camera(resX, resY);
            configureCamera(camera);

            // Setup canvas.
            canvas.Size(resX, resY);

            // Render image.
            var canvasTarget = new CanvasRenderTarget(canvas);
            var renderer = new RayTracerRenderer();

            while (true)
            {
                // Rotate the whole scene.
                var rotate = Matrix4x4.RotateY(-0.523599f / 2f);

                foreach (var g in scene.Geometries)
                {
                    g.Transform(rotate);
                }

                // Render, and measure time.
                var timeBefore = DateTime.Now;
                renderer.Render(scene, camera, canvasTarget, renderSettings);
                var timeAfter = DateTime.Now;

                // Print time to render.
                var timeElapsed = timeAfter - timeBefore;
                Console.WriteLine("time to render: " + timeElapsed);

                // Wait for user.
                Console.ReadLine();
            }
        }
    }
}
