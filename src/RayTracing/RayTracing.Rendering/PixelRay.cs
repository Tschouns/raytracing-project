
using RayTracing.Base;

namespace RayTracing.Rendering
{
    /// <summary>
    /// Contains a ray with the associated pixel coordinates.
    /// </summary>
    public class PixelRay
    {
        public PixelRay(Ray ray, int x, int y)
        {
            Argument.AssertNotNull(ray, nameof(ray));

            Ray = ray;
            X = x;
            Y = y;
        }

        public Ray Ray { get; }
        public int X { get; }
        public int Y { get; }
    }
}
