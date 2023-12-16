using RayTracing.Base;
using RayTracing.Math;

namespace RayTracing.Model
{
    /// <summary>
    /// Represents a 3D geometry.
    /// </summary>
    public class Geometry
    {
        public Geometry(string name, Material material, IReadOnlyList<Face> faces, AxisAlignedBoundingBox boundingBox)
        {
            Argument.AssertNotNull(name, nameof(name));
            Argument.AssertNotNull(material, nameof(material));
            Argument.AssertNotNull(faces, nameof(faces));
            Argument.AssertNotNull(boundingBox, nameof(boundingBox));

            Name = name;
            Material = material;
            Faces = faces;
            BoundingBox = boundingBox;
        }

        /// <summary>
        /// Gets the geometry name (mainly for debugging purposes).
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the material.
        /// </summary>
        public Material Material { get; }

        /// <summary>
        /// Gets all the mesh's faces.
        /// </summary>
        public IReadOnlyList<Face> Faces { get; private set; }

        /// <summary>
        /// Gets the axis-aligned bounding box.
        /// </summary>
        public AxisAlignedBoundingBox BoundingBox { get; private set; }

        /// <summary>
        /// Transforms all the faces of this geometry using the specified transformation matrix.
        /// </summary>
        /// <param name="t">
        /// The transformation matrix
        /// </param>
        public void Transform(Matrix4x4 t)
        {
            var newFaces = Faces
                .Select(f =>
                    new Face(
                        this,
                        new Triangle3D(
                            t.ApplyTo(f.Triangle.CornerA),
                            t.ApplyTo(f.Triangle.CornerB),
                            t.ApplyTo(f.Triangle.CornerC)),
                        t.ApplyTo(f.Normal)))
                .ToList();

            Faces = newFaces;
        }
    }
}