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
                .Select(pixel => Task.Run(() => SetPixel(faces, pixel, target)))
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
            IEnumerable<Face> faces,
            PixelRay pixel,
            IRenderTarget target)
        {
            var color = DetermineColorRecursive(pixel.Ray, faces);
            target.SetPixel(pixel.X, pixel.Y, color);
        }

        private static Color DetermineColorRecursive(Ray ray, IEnumerable<Face> faces, int depth = 0)
        {
            Argument.AssertNotNull(ray, nameof(ray));
            Argument.AssertNotNull(faces, nameof(faces));

            if (depth > 10)
            {
                return Color.Black;
            }

            var hit = ray.DetectNearestHit(faces);
            if (hit == null)
            {
                return Color.White;
            }

            var baseColor = hit.Face.ParentGeometry.Material.BaseColor;
            var color = FogColor(baseColor, hit.Distance, 20f);

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
    }
}
