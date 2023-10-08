
using RayTracing.Math;

namespace RayTracing.Rendering
{
    /// <summary>
    /// Represents a single ray to trace through a scene.
    /// </summary>
    public class Ray
    {
        public Ray(Vector3 origin, Vector3 direction)
        {
            Origin = origin;
            Direction = direction;
        }

        public Vector3 Origin { get; set; }
        public Vector3 Direction { get; set; }
    }
}