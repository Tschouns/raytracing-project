using RayTracing.Base;
using RayTracing.Math;
using System.Drawing;

namespace RayTracing.Rendering
{
    public class SimpleRenderer : IRender
    {
        public void Render(IEnumerable<Triangle3D> triangles, ICamera camera, IRenderTarget target)
        {
            Argument.AssertNotNull(triangles, nameof(triangles));
            Argument.AssertNotNull(camera, nameof(camera));
            Argument.AssertNotNull(target, nameof(target));

            var pixelRays = camera.GeneratePixelRays();

            // Render image.
            foreach (var pixel in pixelRays)
            {
                SetPixel(triangles, pixel, target);
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
