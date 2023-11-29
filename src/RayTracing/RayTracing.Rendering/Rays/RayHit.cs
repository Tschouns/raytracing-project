using RayTracing.Math;
using RayTracing.Model;

namespace RayTracing.Rendering.Rays
{
    /// <summary>
    /// Represents a ray hit.
    /// </summary>
    public class RayHit
    {
        public RayHit(Vector3 position, Vector3 direction, float distance, Face face, bool isBackFaceHit)
        {
            Position = position;
            Direction = direction;
            Distance = distance;
            Face = face;
            IsBackFaceHit = isBackFaceHit;
        }

        public Vector3 Position { get; }
        public Vector3 Direction { get; }
        public float Distance { get; }
        public Face Face { get; }
        public bool IsBackFaceHit { get; }
    }
}
