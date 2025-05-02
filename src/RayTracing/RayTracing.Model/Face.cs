using RayTracing.Base;
using RayTracing.Math;

namespace RayTracing.Model
{
    /// <summary>
    /// Represents a triangular face of 3D geometry.
    /// </summary>
    public class Face
    {
        public Face(Geometry parent, Triangle3D triangle, Vector3 normal)
        {
            Argument.AssertNotNull(parent, nameof(parent));
            Argument.AssertNotNull(triangle, nameof(triangle));

            this.ParentGeometry = parent;
            this.Triangle = triangle;
            this.Normal = normal;
        }

        /// <summary>
        /// Gets the geometry this face belongs to.
        /// </summary>
        public Geometry ParentGeometry { get; }

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
