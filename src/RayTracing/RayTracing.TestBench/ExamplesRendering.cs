using RayTracing.CanvasClient;
using RayTracing.Math;
using RayTracing.Rendering.Settings;
using System.Drawing;

namespace RayTracing.TestBench
{
    public static class ExamplesRendering
    {
        public static void DummyScene(ICanvas canvas)
        {
            ExampleUtils.RenderToCanvas(
                canvas,
                @"..\..\..\..\..\..\models\dummy\dummy.dae",
                scene =>
                {
                    var planeMaterial = scene.Materials.Single(m => m.Name.Contains("Plane"));
                    planeMaterial.Reflectivity = 0.2f;

                    var cubeMaterial = scene.Materials.Single(m => m.Name.Contains("Cube"));
                    cubeMaterial.Transparency = 0.2f;

                    var cylinderMaterial = scene.Materials.Single(m => m.Name.Contains("Cylinder"));
                    cylinderMaterial.Reflectivity = 0;
                    cylinderMaterial.Transparency = 0;
                },
                resX: 400,
                resY: 300,
                camera =>
                {
                    camera.Position = new Vector3(0, 2.5f, -4f);
                    camera.LookingDirection = new Vector3(0, -0.3f, 0.8f);
                    camera.FocalLength /= 2f;
                },
                new RenderSettings
                {
                    FillBackground = true,
                    ApplyDepthCueing = true,
                    ApplyNormalShading = true,
                    ApplyShadows = true,
                    ApplyReflections = true,
                    ApplyTransmission = true,
                    DepthCueingMaxDistance = 100f,
                    MaxRecursionDepth = 5,
                });
        }

        public static void GlassScene(ICanvas canvas)
        {
            ExampleUtils.RenderToCanvas(
                canvas,
                @"..\..\..\..\..\..\models\glass\glass.dae",
                scene =>
                {
                    var glassMaterial = scene.Materials.Single(m => m.Name.Contains("Glass"));
                    glassMaterial.BaseColor = Color.GreenYellow;
                    glassMaterial.Reflectivity = 0.8f;
                },
                resX: 300,
                resY: 300,
                camera =>
                {
                    camera.Position = new Vector3(0, 1f, -1.3f);
                    camera.LookingDirection = new Vector3(0, -0.5f, 0.7f);
                    camera.FocalLength *= 1.5f;
                },
                new RenderSettings
                {
                    FillBackground = false,
                    DepthCueingMaxDistance = 20f,
                });
        }
    }
}
