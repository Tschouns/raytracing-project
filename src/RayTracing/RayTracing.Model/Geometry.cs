using RayTracing.Base;
using RayTracing.Math;
using System.Drawing;

namespace RayTracing.Model
{
    /// <summary>
    /// Represents a 3D geometry.
    /// </summary>
    public class Geometry
    {
        public Geometry(string name, Color baseColor, IReadOnlyList<Face> faces)
        {
            Argument.AssertNotNull(name, nameof(name));
            Argument.AssertNotNull(faces, nameof(faces));

            Name = name;
            BaseColor = baseColor;
            Faces = faces;
        }

        /// <summary>
        /// Gets the geometry name (mainly for debugging purposes).
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the geometry base color.
        /// </summary>
        public Color BaseColor { get; }

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
                        this,
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