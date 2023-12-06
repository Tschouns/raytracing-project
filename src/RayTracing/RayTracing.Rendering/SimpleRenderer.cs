using RayTracing.Base;
using RayTracing.Model;
using RayTracing.Rendering.Cameras;
using RayTracing.Rendering.Helpers;
using RayTracing.Rendering.Rays;
using RayTracing.Rendering.Settings;
using RayTracing.Rendering.Targets;
using System.Drawing;

namespace RayTracing.Rendering
{
    /// <summary>
    /// A very simple implementation of <see cref="IRender"/>.
    /// </summary>
    public class SimpleRenderer : IRender
    {
        public void Render(Scene scene, ICamera camera, IRenderTarget target, IRenderSettings settings)
        {
            Argument.AssertNotNull(scene, nameof(scene));
            Argument.AssertNotNull(camera, nameof(camera));
            Argument.AssertNotNull(target, nameof(target));
            Argument.AssertNotNull(settings, nameof(settings));

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
                .Select(pixel => Task.Run(() => SetPixel(faces, scene.LightSources, pixel, target, settings)))
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
            IRenderTarget target,
            IRenderSettings settings)
        {
            Argument.AssertNotNull(allFaces, nameof(allFaces));
            Argument.AssertNotNull(lightSources, nameof(lightSources));
            Argument.AssertNotNull(pixel, nameof(pixel));
            Argument.AssertNotNull(target, nameof(target));
            Argument.AssertNotNull(settings, nameof(settings));

            var color = DetermineColorRecursive(pixel.Ray, allFaces, lightSources, settings);
            target.SetPixel(pixel.X, pixel.Y, color);
        }

        private static Color DetermineColorRecursive(
            Ray ray,
            IEnumerable<Face> allFaces,
            IEnumerable<LightSource> lightSources,
            IRenderSettings settings,
            int depth = 0)
        {
            Argument.AssertNotNull(ray, nameof(ray));
            Argument.AssertNotNull(allFaces, nameof(allFaces));
            Argument.AssertNotNull(lightSources, nameof(lightSources));
            Argument.AssertNotNull(settings, nameof(settings));

            if (depth > settings.MaxRecursionDepth)
            {
                return settings.DepthCueingColor;
            }

            var hit = ray.DetectNearestHit(allFaces);
            if (hit == null)
            {
                return settings.DepthCueingColor;
            }

            var baseColor = hit.Face.ParentGeometry.Material.BaseColor;

            // Check light sources.
            var totalLightColor = settings.AmbientLightColor;

            foreach (var light in lightSources)
            {
                var lightSourceCheckRay = new Ray(hit.Position, light.Location - hit.Position);
                if (!lightSourceCheckRay.HasAnyHit(allFaces, exclude: hit.Face))
                {
                    var lightFactor = hit.Face.Normal.Dot(lightSourceCheckRay.Direction.Norm()!.Value);
                    var additionalLight = ColorUtils.Scale(light.Color, lightFactor);
                    totalLightColor = ColorUtils.Add(totalLightColor, additionalLight);
                }
            }

            var litColor = ColorUtils.Multiply(baseColor, totalLightColor);

            // Add depth fog.
            var color = FogColor(litColor, settings.DepthCueingColor, hit.Distance, settings.DepthCueingMaxDistance);

            return color;
        }

        private static Color FogColor(Color baseColor, Color fadeColor, float distance, float maxDistance)
        {
            var fadeFactor = System.Math.Clamp(distance / maxDistance, 0, 1);
            
            var fadeColorFraction = ColorUtils.Scale(fadeColor, fadeFactor);
            var baseColorFraction = ColorUtils.Scale(baseColor, 1 - fadeFactor);

            var newColor = ColorUtils.Add(baseColorFraction, fadeColorFraction);

            return newColor;
        }
    }
}
