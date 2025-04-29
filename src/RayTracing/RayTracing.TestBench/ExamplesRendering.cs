using RayTracing.Math;
using RayTracing.Model;
using RayTracing.Rendering.Settings;
using RayTracing.Targets;
using System.Drawing;

namespace RayTracing.TestBench
{
    public static class ExamplesRendering
    {
        public static void DummyScene(IRenderTarget target, ushort resX, ushort resY)
        {
            ExampleUtils.RenderToCanvas(
                target,
                @"..\..\..\..\..\..\models\dummy\dummy.dae",
                scene =>
                {
                    var planeMaterial = scene.Materials.Single(m => m.Name.Contains("Plane"));
                    planeMaterial.BaseColor = Color.White.ToArgbColor();
                    planeMaterial.Reflectivity = 0.1f;

                    var cubeMaterial = scene.Materials.Single(m => m.Name.Contains("Cube"));
                    cubeMaterial.Transparency = 0.3f;

                    var cylinderMaterial = scene.Materials.Single(m => m.Name.Contains("Cylinder"));
                    cylinderMaterial.Reflectivity = 0.8f;
                    cylinderMaterial.Transparency = 0.5f;
                },
                resX: resX,
                resY: resY,
                camera =>
                {
                    camera.Position = new Vector3(0, 2.5f, -4f);
                    camera.LookingDirection = new Vector3(0, -0.3f, 0.8f);
                    camera.FocalLength /= 2.5f;
                },
                new RenderSettings
                {
                    FillBackground = false,
                    //ApplyDepthCueing = false,
                    //ApplyNormalShading = false,
                    //ApplyShadows = false,
                    //ApplyReflections = false,
                    //ApplyTransmission = false,
                    ApplyGloss = true,
                    DepthCueingMaxDistance = 100f,
                    MaxRecursionDepth = 5,
                });
        }

        public static void GlassScene(IRenderTarget target, ushort resX, ushort resY)
        {
            ExampleUtils.RenderToCanvas(
                target,
                @"..\..\..\..\..\..\models\glass\glass.dae",
                scene =>
                {
                    var tableMaterial = scene.Materials.Single(m => m.Name.Contains("Table"));
                    tableMaterial.BaseColor = Color.LightSteelBlue.ToArgbColor();
                    tableMaterial.Reflectivity = 0.5f;

                    var glassMaterial = scene.Materials.Single(m => m.Name.Contains("Glass"));
                    glassMaterial.Reflectivity = 0.8f;
                    glassMaterial.Glossyness = 0.9f;
                    glassMaterial.Transparency = 0.8f;

                    var bottleMaterial = scene.Materials.Single(m => m.Name.Contains("Bottle"));
                    bottleMaterial.Reflectivity = 0.6f;
                    bottleMaterial.Glossyness = 0.9f;
                    bottleMaterial.Transparency = 0.5f;
                },
                resX: resX,
                resY: resY,
                camera =>
                {
                    camera.Position = new Vector3(0, 1f, -1.3f);
                    camera.LookingDirection = new Vector3(0, -0.5f, 1f);
                    //camera.FocalLength *= 1.1f;
                },
                new RenderSettings
                {
                    FillBackground = true,
                    ApplyDepthCueing = true,
                    ApplyNormalShading = true,
                    ApplyShadows = false,
                    ApplyReflections = false,
                    ApplyTransmission = false,
                    ApplyGloss = false,
                    UseFancyLighting = true,
                    DepthCueingMaxDistance = 20f,
                    MaxRecursionDepth = 5,
                });
        }
    }
}
