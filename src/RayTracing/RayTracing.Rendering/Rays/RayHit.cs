using RayTracing.Math;
using RayTracing.Model;

namespace RayTracing.Rendering.Rays
{
    /// <summary>
    /// Represents a ray hit.
    /// </summary>
    public class RayHit
    {
        public RayHit(Face face, Vector3 position, float distance)
        {
            Face = face;
            Position = position;
            Distance = distance;
        }

        public Vector3 Position { get; }
        public float Distance { get; }
        public Face Face { get; }
    }
}
