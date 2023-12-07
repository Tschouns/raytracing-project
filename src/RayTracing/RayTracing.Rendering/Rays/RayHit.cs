using RayTracing.Math;
using RayTracing.Model;

namespace RayTracing.Rendering.Rays
{
    /// <summary>
    /// Represents a ray hit.
    /// </summary>
    public class RayHit
    {
        public RayHit(Face face, Vector3 position, Vector3 direction, float distance)
        {
            Face = face;
            Position = position;
            Direction = direction;
            Distance = distance;
        }

        public Vector3 Position { get; }
        public Vector3 Direction { get; }
        public float Distance { get; }
        public Face Face { get; }
    }
}
