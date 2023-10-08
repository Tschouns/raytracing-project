using RayTracing.Base;
using RayTracing.Math;
using System.Drawing;

namespace RayTracing.Rendering
{
    /// <summary>
    /// A very simple implementation of <see cref="IRender"/>.
    /// </summary>
    public class SimpleRenderer : IRender
    {
        public void Render(IEnumerable<Triangle3D> triangles, ICamera camera, IRenderTarget target)
        {
            Argument.AssertNotNull(triangles, nameof(triangles));
            Argument.AssertNotNull(camera, nameof(camera));
            Argument.AssertNotNull(target, nameof(target));

            var pixelRays = camera.GeneratePixelRays();
            var pixelRaysList = pixelRays.ToList();
            Shuffle(pixelRaysList);
            pixelRays = pixelRaysList;

            // Render image.
            target.Fill(Color.CornflowerBlue);
            var tasks = pixelRays
                .Select(pixel => Task.Run(() => SetPixel(triangles, pixel, target)))
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
            IEnumerable<Triangle3D> triangles,
            PixelRay pixel,
            IRenderTarget target)
        {
            var hit = pixel.Ray.DetectNearestHit(triangles);
            if (hit == null)
            {
                target.SetPixel(pixel.X, pixel.Y, Color.White);
                return;
            }

            var color = FogColor(Color.Red, hit.Distance, 20f);
            target.SetPixel(pixel.X, pixel.Y, color);
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
