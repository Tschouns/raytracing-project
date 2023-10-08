using RayTracing.Base;
using RayTracing.Math;

namespace RayTracing.Model
{
    /// <summary>
    /// Represents a triangular face of 3D geometry.
    /// </summary>
    public class Face
    {
        public Face(Triangle3D triangle, Vector3 normal)
        {
            Argument.AssertNotNull(triangle, nameof(triangle));

            Triangle = triangle;
            Normal = normal;
        }

        public Triangle3D Triangle { get; }
        public Vector3 Normal { get; }
    }
}
