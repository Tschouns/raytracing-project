using RayTracing.Base;
using RayTracing.ModelFiles.ColladaFormat;
using RayTracing.Rendering.Cameras;
using RayTracing.Rendering.Settings;
using RayTracing.Rendering;
using RayTracing.Math;
using RayTracing.Model;
using RayTracing.Targets;

namespace RayTracing.TestBench
{
    internal static class ExampleUtils
    {
        public static void RenderToCanvas(
            IRenderTarget target,
            string colladaFile,
            Action<Scene> configureScene,
            ushort resX,
            ushort resY,
            Action<ICameraSettings> configureCamera,
            IRenderSettings renderSettings)
        {
            Argument.AssertNotNull(target, nameof(target));
            Argument.AssertNotNull(colladaFile, nameof(colladaFile));
            Argument.AssertNotNull(configureScene, nameof(configureScene));
            Argument.AssertNotNull(configureCamera, nameof(configureCamera));
            Argument.AssertNotNull(renderSettings, nameof(renderSettings));

            // Load a model.
            var parser = new ColladaFileParser();
            var scene = parser.LoadFromFile(colladaFile);

            configureScene(scene);

            // Setup camera.
            var camera = new Camera(resX, resY);
            configureCamera(camera);

            // Render image.
            var renderer = new RayTracerRenderer();

            // Render, and measure time.
            var timeBefore = DateTime.Now;
            renderer.Render(scene, camera, target, renderSettings);
            var timeAfter = DateTime.Now;

            // Print time to render.
            var timeElapsed = timeAfter - timeBefore;
            Console.WriteLine("time to render: " + timeElapsed);
        }
    }
}
