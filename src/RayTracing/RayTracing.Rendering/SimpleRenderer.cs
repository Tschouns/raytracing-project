using RayTracing.Base;
using RayTracing.Model;
using RayTracing.Rendering.Cameras;
using RayTracing.Rendering.Rays;
using RayTracing.Rendering.Targets;
using System.Drawing;

namespace RayTracing.Rendering
{
    /// <summary>
    /// A very simple implementation of <see cref="IRender"/>.
    /// </summary>
    public class SimpleRenderer : IRender
    {
        private static float colorFactor = 1 / (255 * 255);

        public void Render(Scene scene, ICamera camera, IRenderTarget target)
        {
            Argument.AssertNotNull(scene, nameof(scene));
            Argument.AssertNotNull(camera, nameof(camera));
            Argument.AssertNotNull(target, nameof(target));

            var faces = scene.Geometries
                .SelectMany(g => g.Faces)
                .ToList();

            var pixelRays = camera.GeneratePixelRays();
            var pixelRaysList = pixelRays.ToList();
            Shuffle(pixelRaysList);
            pixelRays = pixelRaysList;

            // Render image.
            target.Fill(Color.CornflowerBlue);
            var tasks = pixelRays
                .Select(pixel => Task.Run(() => SetPixel(faces, scene.LightSources, pixel, target)))
                .ToArray();

            Task.WaitAll(tasks);
        }

        private static void Shuffle<T>(IList<T> list)
        {
            var rng = new Random();
            int n = list.Count;

            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        private static void SetPixel(
            IEnumerable<Face> allFaces,
            IEnumerable<LightSource> lightSources,
            PixelRay pixel,
            IRenderTarget target)
        {
            Argument.AssertNotNull(allFaces, nameof(allFaces));
            Argument.AssertNotNull(lightSources, nameof(lightSources));
            Argument.AssertNotNull(pixel, nameof(pixel));
            Argument.AssertNotNull(target, nameof(target));

            var color = DetermineColorRecursive(pixel.Ray, allFaces, lightSources);
            target.SetPixel(pixel.X, pixel.Y, color);
        }

        private static Color DetermineColorRecursive(
            Ray ray,
            IEnumerable<Face> allFaces,
            IEnumerable<LightSource> lightSources,
            int depth = 0)
        {
            Argument.AssertNotNull(ray, nameof(ray));
            Argument.AssertNotNull(allFaces, nameof(allFaces));
            Argument.AssertNotNull(lightSources, nameof(lightSources));

            if (depth > 10) // TODO settings -> max depth
            {
                return Color.Black;
            }

            var hit = ray.DetectNearestHit(allFaces);
            if (hit == null)
            {
                return Color.White;
            }

            var baseColor = hit.Face.ParentGeometry.Material.BaseColor;

            // Check light sources.
            var totalLightColor = Color.Black; // TODO: settings -> ambient light
            foreach (var light in lightSources)
            {
                var lightSourceCheckRay = new Ray(hit.Position, light.Location - hit.Position);
                if (!lightSourceCheckRay.HasAnyHit(allFaces, exclude: hit.Face))
                {
                    totalLightColor = AddColors(totalLightColor, light.Color);
                }
            }

            var litColor = MultiplyColors(baseColor, totalLightColor);

            // Add depth fog.
            var color = FogColor(litColor, hit.Distance, 20f); // TODO settings -> maxdistance

            return color;
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

        private static Color AddColors(Color colorA, Color colorB)
        {
            return Color.FromArgb(
                System.Math.Min(colorA.R + colorB.R, 255),
                System.Math.Min(colorA.G + colorB.G, 255),
                System.Math.Min(colorA.B + colorB.B, 255));
        }

        private static Color MultiplyColors(Color colorA, Color colorB)
        {
            return Color.FromArgb(
                MultiplyColorValues(colorA.R, colorB.R),
                MultiplyColorValues(colorA.G, colorB.G),
                MultiplyColorValues(colorA.B, colorB.B));
        }

        private static byte MultiplyColorValues(byte valueA, byte valueB)
        {
            var factorA = valueA / 255f;
            var factorB = valueB / 255f;

            var newValue = factorA * factorB * 255;

            return Convert.ToByte(newValue);
        }
    }
}
