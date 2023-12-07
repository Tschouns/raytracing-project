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

            var baseColor = hit.Face.ParentGeometry.Material.BaseColor;

            // Check light sources.
            var totalLightColor = settings.AmbientLightColor;

            foreach (var light in lightSources)
            {
                var lightSourceCheckRay = new Ray(hit.Position, light.Location - hit.Position, originFace: hit.Face);
                
                if (!lightSourceCheckRay.HasAnyHit(allFaces))
                {
                    var lightFactor = hit.Face.Normal.Dot(lightSourceCheckRay.Direction.Norm()!.Value);
                    var additionalLight = ColorUtils.Scale(light.Color, lightFactor);
                    totalLightColor = ColorUtils.Add(totalLightColor, additionalLight);
                }
            }

            var litColor = ColorUtils.Multiply(baseColor, totalLightColor);

            // Get reflection.
            var n = AsMatrix(hit.Face.Normal);
            var nT = n.Transpose();
            var nnT = n.Multiply(nT);
            var nnTm2 = nnT.Multiply(-2);
            var reflectTransform = Matrix4x4.Identity.Add(nnTm2);
            var outgoingDirection = reflectTransform.ApplyTo(ray.Direction);

            var reflectionRay = new Ray(hit.Position, outgoingDirection, originFace: hit.Face);
            var reflectionColor = DetermineColorRecursive(
                reflectionRay,
                allFaces,
                lightSources,
                settings,
                depth + 1);

            // TODO: get the reflection property from the collada file...
            var reflectiviy = hit.Face.ParentGeometry.Material.Reflectivity;
            var colorWithReflection = ColorUtils.Add(
                ColorUtils.Scale(litColor, 1 - reflectiviy),
                ColorUtils.Scale(reflectionColor, reflectiviy));

            // Add depth fog.
            var color = FogColor(colorWithReflection, settings.DepthCueingColor, hit.Distance, settings.DepthCueingMaxDistance);

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
