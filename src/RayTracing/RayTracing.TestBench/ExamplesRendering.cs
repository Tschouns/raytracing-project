﻿using RayTracing.Base;
using RayTracing.CanvasClient;
using RayTracing.Math;
using RayTracing.Rendering;
using System.Drawing;

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

            var pixelRays = camera.GetRasterRays();

            // Render image.
            canvas.Size(resX, resY);

            foreach (var pixel in pixelRays)
            {
                SetPixel(triables, pixel, canvas);
            }
        }

        private static void SetPixel(
            IEnumerable<Triangle3D> triangles,
            PixelRay pixel, ICanvas canvas)
        {
            var hit = pixel.Ray.DetectNearestHit(triangles);
            if (hit == null)
            {
                canvas.Pixel(pixel.X, pixel.Y, Color.White);
                return;
            }

            var color = FogColor(Color.Red, hit.Distance, 20f);
            canvas.Pixel(pixel.X, pixel.Y, color);
        }

        private static Color FogColor(Color baseColor, float distance, float maxDistance)
        {
            var f = 1 - System.Math.Clamp(distance / maxDistance, 0, 1);
            var newColor = Color.FromArgb(
                (byte)(baseColor.R * f),
                (byte)(baseColor.G * f),
                (byte)(baseColor.B * f));

            return newColor;
        }
    }
}