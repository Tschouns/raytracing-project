using RayTracing.Base;
using RayTracing.CanvasClient;
using RayTracing.Math;
using RayTracing.ModelFiles.ColladaFormat;
using RayTracing.Rendering;
using RayTracing.Rendering.Cameras;
using RayTracing.Rendering.Settings;
using System.Drawing;

namespace RayTracing.TestBench
{
    public static class ExamplesRendering
    {
        public static void GlassScene(ICanvas canvas)
        {
            ExampleUtils.RenderToCanvas(
                canvas,
                @"..\..\..\..\..\..\models\glass\glass.dae",
                scene =>
                {
                    var glassMaterial = scene.Materials.Single(m => m.Name.Contains("Glass"));
                    glassMaterial.Reflectivity = 0.5f;
                },
                resX: 90,
                resY: 120,
                camera =>
                {
                    camera.Position = new Vector3(0, 1f, -1.3f);
                    camera.LookingDirection = new Vector3(0, -0.5f, 0.8f);
                },
                new RenderSettings
                {
                    FillBackground = false,
                    DepthCueingMaxDistance = 200f,
                });
        }

        public static void Camera(ICanvas canvas)
        {
            Argument.AssertNotNull(canvas, nameof(canvas));

            // Load a model.
            var parser = new ColladaFileParser();
            //var scene = parser.LoadFromFile(@"..\..\..\..\..\..\models\dummy\dummy.dae");
            var scene = parser.LoadFromFile(@"..\..\..\..\..\..\models\glass\glass.dae");

            // Setup camera.
            ushort resX = 300;
            ushort resY = 400;

            var camera = new Camera(resX, resY);
            camera.Position = new Vector3(0, 1f, -1.3f);
            camera.LookingDirection = new Vector3(0, -0.5f, 0.8f);
            //camera.FocalLength /= 1.5f;

            // Setup canvas.
            canvas.Size(resX, resY);

            // Render image.
            var inMemoryTarget = new InMemoryRenderTarget(resX, resY);
            var canvasTarget = new CanvasRenderTarget(canvas);
            var renderer = new RayTracerRenderer();

            while (true)
            {
                var rotate = Matrix4x4.RotateY(-0.523599f / 2f);

                foreach (var g in scene.Geometries)
                {
                    g.Transform(rotate);
                }

                var timeBefore = DateTime.Now;
                renderer.Render(scene, camera, canvasTarget, new RenderSettings
                {
                    FillBackground = false,
                    FillBackgroundColor = Color.RebeccaPurple,
                    //AmbientLightColor = Color.DarkSlateBlue,
                    //DepthCueingColor = Color.AliceBlue,
                    DepthCueingMaxDistance = 100,
                    MaxRecursionDepth = 10,
                });
                var timeAfter = DateTime.Now;
                var timeElapsed = timeAfter - timeBefore;
                Console.WriteLine("time to render: " + timeElapsed);

                //inMemoryTarget.ApplyImageToTarget(canvasTarget);

                Console.ReadLine();
            }
        }
    }
}
