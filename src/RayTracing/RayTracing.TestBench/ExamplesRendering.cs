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
                    planeMaterial.BaseColor = Color.White;
                    planeMaterial.Reflectivity = 0.1f;

                    var cubeMaterial = scene.Materials.Single(m => m.Name.Contains("Cube"));
                    cubeMaterial.Transparency = 0.3f;

                    var cylinderMaterial = scene.Materials.Single(m => m.Name.Contains("Cylinder"));
                    cylinderMaterial.Reflectivity = 0.8f;
                    cylinderMaterial.Transparency = 0.5f;
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
                    FillBackground = false,
                    //ApplyDepthCueing = false,
                    //ApplyNormalShading = false,
                    //ApplyShadows = false,
                    //ApplyReflections = false,
                    //ApplyTransmission = false,
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
                    var tableMaterial = scene.Materials.Single(m => m.Name.Contains("Table"));
                    tableMaterial.BaseColor = Color.WhiteSmoke;
                    tableMaterial.Reflectivity = 0.3f;

                    var glassMaterial = scene.Materials.Single(m => m.Name.Contains("Glass"));
                    glassMaterial.BaseColor = Color.GreenYellow;
                    glassMaterial.Reflectivity = 0.8f;
                    glassMaterial.Transparency = 0.75f;
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
                    FillBackground = true,
                    ApplyDepthCueing = true,
                    ApplyNormalShading = true,
                    ApplyShadows = true,
                    ApplyReflections = true,
                    ApplyTransmission = true,
                    DepthCueingMaxDistance = 20f,
                    MaxRecursionDepth = 5,
                });
        }
    }
}
