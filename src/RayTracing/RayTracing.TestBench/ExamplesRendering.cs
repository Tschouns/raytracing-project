﻿using RayTracing.CanvasClient;
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

        public static void GlassScene(ICanvas canvas)
        {
            ExampleUtils.RenderToCanvas(
                canvas,
                @"..\..\..\..\..\..\models\glass\glass.dae",
                scene =>
                {
                    var tableMaterial = scene.Materials.Single(m => m.Name.Contains("Table"));
                    tableMaterial.BaseColor = Color.LightSteelBlue;
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
                resX: 200,
                resY: 240,
                camera =>
                {
                    camera.Position = new Vector3(0, 1f, -1.3f);
                    camera.LookingDirection = new Vector3(0, -0.5f, 1f);
                    //camera.FocalLength *= 1.1f;
                },
                new RenderSettings
                {
                    //FillBackground = false,
                    //ApplyDepthCueing = false,
                    //ApplyNormalShading = false,
                    //ApplyShadows = false,
                    //ApplyReflections = false,
                    //ApplyTransmission = false,
                    //ApplyGloss = false,
                    //UseFancyLighting = false,
                    DepthCueingMaxDistance = 20f,
                    MaxRecursionDepth = 5,
                });
        }
    }
}
