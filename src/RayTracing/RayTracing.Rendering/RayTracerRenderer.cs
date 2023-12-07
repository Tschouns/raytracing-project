using RayTracing.Base;
using RayTracing.Math;
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
    /// A very simple ray tracer <see cref="IRender"/>.
    /// </summary>
    public class RayTracerRenderer : IRender
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

            // Fill background.
            if (settings.FillBackground)
            {
                target.Fill(settings.FillBackgroundColor);
            }

            // Render image pixel by pixel.
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

            // Start with the base color...
            var pixelColor = hit.Face.ParentGeometry.Material.BaseColor;

            if (settings.ApplyDepthCueing)
            {
                pixelColor = ApplyDepthCueing(pixelColor, settings.DepthCueingColor, hit.Distance, settings.DepthCueingMaxDistance);
            }

            if (settings.ApplyNormalShading)
            {
                pixelColor = ApplyNormalShading(pixelColor, hit, lightSources, settings);
            }

            if (settings.ApplyShadows)
            {
                pixelColor = ApplyShadows(pixelColor, hit, allFaces, lightSources, settings);
            }

            if (settings.ApplyReflections)
            {
                pixelColor = ApplyReflectionRecursive(pixelColor, hit, allFaces, lightSources, settings, depth);
            }

            return pixelColor;
        }

        private static Color ApplyDepthCueing(Color baseColor, Color fadeColor, float distance, float maxDistance)
        {
            var fadeFactor = System.Math.Clamp(distance / maxDistance, 0, 1);

            var fadeColorFraction = ColorUtils.Scale(fadeColor, fadeFactor);
            var baseColorFraction = ColorUtils.Scale(baseColor, 1 - fadeFactor);

            var newColor = ColorUtils.Add(baseColorFraction, fadeColorFraction);

            return newColor;
        }

        private static Color ApplyNormalShading(
            Color baseColor,
            RayHit hit,
            IEnumerable<LightSource> lightSources,
            IRenderSettings settings)
        {
            var totalLightColor = settings.AmbientLightColor;

            foreach (var light in lightSources)
            {
                var lightSourceDirection = (light.Location - hit.Position).Norm()!.Value;
                var lightFactor = hit.Face.Normal.Dot(lightSourceDirection);
                var additionalLight = ColorUtils.Scale(light.Color, lightFactor);

                totalLightColor = ColorUtils.Add(totalLightColor, additionalLight);
            }

            var litColor = ColorUtils.Multiply(baseColor, totalLightColor);

            return litColor;
        }

        private static Color ApplyShadows(
            Color baseColor,
            RayHit hit,
            IEnumerable<Face> allFaces,
            IEnumerable<LightSource> lightSources,
            IRenderSettings settings)
        {
            var totalLightColor = settings.AmbientLightColor;

            foreach (var light in lightSources)
            {
                var lightSourceCheckRay = new Ray(hit.Position, light.Location - hit.Position, originFace: hit.Face);
                if (!lightSourceCheckRay.HasAnyHit(allFaces))
                {
                    totalLightColor = ColorUtils.Add(totalLightColor, light.Color);
                }
            }

            var litColor = ColorUtils.Multiply(baseColor, totalLightColor);

            return litColor;
        }

        private static Color ApplyReflectionRecursive(
            Color baseColor,
            RayHit hit,
            IEnumerable<Face> allFaces,
            IEnumerable<LightSource> lightSources,
            IRenderSettings settings,
            int currentRecursionDepth)
        {
            // Get reflection.
            var n = AsMatrix(hit.Face.Normal);
            var nT = n.Transpose();
            var nnT = n.Multiply(nT);
            var nnTm2 = nnT.Multiply(-2);
            var reflectTransform = Matrix4x4.Identity.Add(nnTm2);
            var outgoingDirection = reflectTransform.ApplyTo(hit.Direction);

            var reflectionRay = new Ray(hit.Position, outgoingDirection, originFace: hit.Face);
            var reflectionColor = DetermineColorRecursive(
                reflectionRay,
                allFaces,
                lightSources,
                settings,
                currentRecursionDepth + 1);

            var reflectivity = hit.Face.ParentGeometry.Material.Reflectivity;
            var colorWithReflection = ColorUtils.Add(
                ColorUtils.Scale(baseColor, 1 - reflectivity),
                ColorUtils.Scale(reflectionColor, reflectivity));

            return colorWithReflection;
        }

        private static Matrix4x4 AsMatrix(Vector3 vector)
        {
            return new Matrix4x4(
                vector.X, 0, 0, 0,
                vector.Y, 0, 0, 0,
                vector.Z, 0, 0, 0,
                0, 0, 0, 0);
        }
    }
}
