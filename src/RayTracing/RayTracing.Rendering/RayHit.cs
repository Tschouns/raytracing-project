using RayTracing.Math;

namespace RayTracing.Rendering
{
    /// <summary>
    /// Represents a ray hit.
    /// </summary>
    public class RayHit
    {
        public RayHit(Vector3 position)
        {
            Position = position;
        }

        public Vector3 Position { get; }
    }
}
