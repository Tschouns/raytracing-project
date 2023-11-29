using RayTracing.Base;
using RayTracing.Model;
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
            var hit = pixel.Ray.DetectNearestHit(faces);
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
