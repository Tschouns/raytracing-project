using RayTracing.Base;

namespace RayTracing.Rendering.Rays
{
    /// <summary>
    /// Contains a ray with the associated pixel coordinates.
    /// </summary>
    public class PixelRay
    {
        public PixelRay(Ray ray, int x, int y)
        {
            Argument.AssertNotNull(ray, nameof(ray));

            this.Ray = ray;
            this.X = x;
            this.Y = y;
        }

        public Ray Ray { get; }
        public int X { get; }
        public int Y { get; }
    }
}
