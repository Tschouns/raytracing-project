using RayTracing.Math;

namespace RayTracing.Rendering
{
    /// <summary>
    /// Represents a ray hit.
    /// </summary>
    public class RayHit
    {
        public RayHit(Vector3 position, float distance)
        {
            Position = position;
            Distance = distance;
        }

        public Vector3 Position { get; }
        public float Distance { get; }
    }
}
