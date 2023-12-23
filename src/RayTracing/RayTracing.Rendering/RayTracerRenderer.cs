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
            if (settings.UseParallelRendering)
            {
                var tasks = pixelRays
                    .Select(pixel => Task.Run(() => SetPixel(scene.Geometries, scene.LightSources, pixel, target, settings)))
                    .ToArray();

                Task.WaitAll(tasks);
            }
            else
            {
                // Render image pixel by pixel.
                foreach (var pixel in pixelRays)
                {
                    SetPixel(scene.Geometries, scene.LightSources, pixel, target, settings);
                }
            }
        }

        private static void Shuffle<T>(IList<T> list)
        {
            var rng = new Random();
            var n = list.Count;

            while (n > 1)
            {
                n--;
                var k = rng.Next(n + 1);
                var value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        private static void SetPixel(
            IEnumerable<Geometry> allGeometries,
            IEnumerable<LightSource> lightSources,
            PixelRay pixel,
            IRenderTarget target,
            IRenderSettings settings)
        {
            Argument.AssertNotNull(allGeometries, nameof(allGeometries));
            Argument.AssertNotNull(lightSources, nameof(lightSources));
            Argument.AssertNotNull(pixel, nameof(pixel));
            Argument.AssertNotNull(target, nameof(target));
            Argument.AssertNotNull(settings, nameof(settings));

            var color = DeterminePixelColorRecursive(pixel.Ray, allGeometries, lightSources, settings);
            target.SetPixel(pixel.X, pixel.Y, color);
        }

        private static Color DeterminePixelColorRecursive(
            Ray ray,
            IEnumerable<Geometry> allGeometries,
            IEnumerable<LightSource> lightSources,
            IRenderSettings settings,
            int depth = 0)
        {
            Argument.AssertNotNull(ray, nameof(ray));
            Argument.AssertNotNull(allGeometries, nameof(allGeometries));
            Argument.AssertNotNull(lightSources, nameof(lightSources));
            Argument.AssertNotNull(settings, nameof(settings));

            if (depth >= settings.MaxRecursionDepth)
            {
                return settings.DepthCueingColor;
            }

            var hit = ray.DetectNearestHit(allGeometries);
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

            if (settings.ApplyReflections)
            {
                pixelColor = ApplyReflectionRecursive(pixelColor, hit, allGeometries, lightSources, settings, depth);
            }

            if (settings.ApplyTransmission)
            {
                pixelColor = ApplyTransmissionRecursive(pixelColor, hit, ray, allGeometries, lightSources, settings, depth);
            }

            // Gloss should be at the end (the rest should be irrelevant).
            if (settings.ApplyShadows || settings.ApplyGloss)
            {
                pixelColor = ApplyLighting(pixelColor, hit, allGeometries, lightSources, settings);
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

        private static Color ApplyReflectionRecursive(
            Color baseColor,
            RayHit hit,
            IEnumerable<Geometry> allGeometries,
            IEnumerable<LightSource> lightSources,
            IRenderSettings settings,
            int currentRecursionDepth)
        {
            // A little optimization:
            if (hit.Face.ParentGeometry.Material.Reflectivity <= 0)
            {
                return baseColor;
            }

            // Get reflection ray.
            var n = AsMatrix(hit.Face.Normal);
            var nT = n.Transpose();
            var nnT = n.Multiply(nT);
            var nnTm2 = nnT.Multiply(-2);
            var reflectTransform = Matrix4x4.Identity.Add(nnTm2);
            var outgoingDirection = reflectTransform.ApplyTo(hit.Direction);

            var reflectionRay = new Ray(hit.Position, outgoingDirection, originFace: hit.Face);

            // Recursive call.
            var reflectionColor = DeterminePixelColorRecursive(
                reflectionRay,
                allGeometries,
                lightSources,
                settings,
                currentRecursionDepth + 1);

            var reflectivity = hit.Face.ParentGeometry.Material.Reflectivity;
            var colorWithReflection = ColorUtils.Add(
                ColorUtils.Scale(baseColor, 1 - reflectivity),
                ColorUtils.Scale(reflectionColor, reflectivity));

            return colorWithReflection;
        }

        private static Color ApplyTransmissionRecursive(
            Color baseColor,
            RayHit hit,
            Ray originalRay,
            IEnumerable<Geometry> allGeometries,
            IEnumerable<LightSource> lightSources,
            IRenderSettings settings,
            int currentRecursionDepth)
        {
            // A little optimization:
            if (hit.Face.ParentGeometry.Material.Transparency <= 0)
            {
                return baseColor;
            }

            var newDirection = hit.Direction; // TODO: use IOR;

            // Keep track of which object(s) we're inside of.
            var insideObjects = originalRay.InsideObjects.ToList();
            if (hit.IsBackFaceHit)
            {
                insideObjects.Remove(hit.Face.ParentGeometry);
            }
            else
            {
                insideObjects.Add(hit.Face.ParentGeometry);
            }

            var transmissionRay = new Ray(hit.Position, newDirection, originFace: hit.Face, insideObjects);

            // Recursive call.
            var refractedColor = DeterminePixelColorRecursive(
                transmissionRay,
                allGeometries,
                lightSources,
                settings,
                currentRecursionDepth + 1);

            var transparency = hit.Face.ParentGeometry.Material.Transparency;
            var colorWithReflection = ColorUtils.Add(
                ColorUtils.Scale(baseColor, 1 - transparency),
                ColorUtils.Scale(refractedColor, transparency));

            return colorWithReflection;
        }

        private static Color ApplyLighting(
            Color baseColor,
            RayHit hit,
            IEnumerable<Geometry> allGeometries,
            IEnumerable<LightSource> lightSources,
            IRenderSettings settings)
        {
            var litColor = baseColor;
            var totalLightColor = settings.AmbientLightColor;
            var totalGlossColor = Color.Black;

            foreach (var light in lightSources)
            {
                var lightColor = DetermineLightColorRecursive(hit, light, allGeometries, settings);
                totalLightColor = ColorUtils.Add(totalLightColor, lightColor);

                if (settings.ApplyGloss)
                {
                    var lightSourceDirection = (light.Location - hit.Position).Norm()!.Value;
                    var normalFactor = System.Math.Abs(hit.Face.Normal.Dot(lightSourceDirection));
                    var glossFactor =
                        normalFactor *
                        normalFactor *
                        normalFactor *
                        normalFactor *
                        normalFactor *
                        hit.Face.ParentGeometry.Material.Glossyness *
                        hit.Face.ParentGeometry.Material.Glossyness;
                    //hit.Face.ParentGeometry.Material.Reflectivity;

                    var glossColor = ColorUtils.Scale(totalLightColor, glossFactor);
                    totalGlossColor = ColorUtils.Add(totalGlossColor, glossColor);
                }
            }

            // Apply shadow total.
            if (settings.ApplyShadows)
            {
                litColor = ColorUtils.Multiply(litColor, totalLightColor);
            }

            // Apply - add on top - gloss.
            litColor = ColorUtils.Add(litColor, totalGlossColor);

            return litColor;
        }

        private static Color DetermineLightColorRecursive(
            RayHit hit,
            LightSource light,
            IEnumerable<Geometry> allGeometries,
            IRenderSettings settings,
            int currentDepth = 0)
        {
            if (currentDepth >= settings.MaxRecursionDepth)
            {
                return Color.Black;
            }

            var toLight = light.Location - hit.Position;
            var lightSourceCheckRay = new Ray(hit.Position, toLight, originFace: hit.Face);
            var nearestHit = lightSourceCheckRay.DetectNearestHit(allGeometries);

            if (nearestHit == null ||
                toLight.LengthSquared() < nearestHit.Distance * nearestHit.Distance) // TODO: add max distance to ray?
            {
                return light.Color;
            }

            // Use simple, non-fancy lighting?
            if (!settings.UseFancyLighting)
            {
                return Color.Black;
            }

            var material = nearestHit.Face.ParentGeometry.Material;
            if (material.Transparency <= 0)
            {
                // No light.
                return Color.Black;
            }

            var colorBefore = DetermineLightColorRecursive(nearestHit, light, allGeometries, settings, currentDepth + 1);
            var colorHueAdjusted = ColorUtils.Add(
                ColorUtils.Scale(colorBefore, material.Transparency),
                ColorUtils.Scale(material.BaseColor, 1 - material.Transparency));

            var colorDampened = ColorUtils.Scale(colorHueAdjusted, material.Transparency);

            return colorDampened;
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
