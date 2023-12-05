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

        /// <summary>
        /// Gets the triangle describing the face.
        /// </summary>
        public Triangle3D Triangle { get; }

        /// <summary>
        /// Gets the face normal vector.
        /// </summary>
        public Vector3 Normal { get; }
    }
}
