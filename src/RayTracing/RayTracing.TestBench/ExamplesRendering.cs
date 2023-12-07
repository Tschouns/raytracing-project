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
                    glassMaterial.BaseColor = Color.GreenYellow;
                    glassMaterial.Reflectivity = 0.9f;
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
                    DepthCueingMaxDistance = 20f,
                });
        }
    }
}
