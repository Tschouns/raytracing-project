using RayTracing.Base;
using RayTracing.Math;

namespace RayTracing.Model
{
    /// <summary>
    /// Represents a 3D geometry.
    /// </summary>
    public class Geometry
    {
        public Geometry(IReadOnlyList<Face> faces)
        {
            Argument.AssertNotNull(faces, nameof(faces));

            Faces = faces;
        }

        /// <summary>
        /// Gets all the mesh's faces.
        /// </summary>
        public IReadOnlyList<Face> Faces { get; private set; }

        /// <summary>
        /// Transforms all the faces of this geometry using the specified transformation matrix.
        /// </summary>
        /// <param name="t">
        /// The transformation matrix
        /// </param>
        public void Transform(Matrix4x4 t)
        {
            var newFaces = this.Faces
                .Select(f =>
                    new Face(
                        new Triangle3D(
                            t.ApplyTo(f.Triangle.CornerA),
                            t.ApplyTo(f.Triangle.CornerB),
                            t.ApplyTo(f.Triangle.CornerC)),
                        t.ApplyTo(f.Normal)))
                .ToList();

            this.Faces = newFaces;
        }
    }
}