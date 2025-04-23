
using RayTracing.Base;
using RayTracing.Math;
using RayTracing.Math.Calculations;
using System.Collections.ObjectModel;

namespace RayTracing.Model.Octrees
{
    public static class OctreeHelper
    {
        public static Octree PrepareOctree(IEnumerable<Face> allFaces)
        {
            Argument.AssertNotNull(allFaces, nameof(allFaces));

            var allVertices = allFaces
                .SelectMany(f => new[] { f.Triangle.CornerA, f.Triangle.CornerB, f.Triangle.CornerC })
                .ToList();

            var minX = allVertices.Min(v => v.X);
            var minY = allVertices.Min(v => v.Y);
            var minZ = allVertices.Min(v => v.Z);

            var maxX = allVertices.Max(v => v.X);
            var maxY = allVertices.Max(v => v.Y);
            var maxZ = allVertices.Max(v => v.Z);

            var min = new Vector3(minX, minY, minZ);
            var max = new Vector3(maxX, maxY, maxZ);
            var boundingBox = new AxisAlignedBoundingBox(min, max);

            var children = PrepOctantsRecursive(boundingBox, allFaces, splitTheshold: 200);

            return new Octree(boundingBox, allFaces, children);
        }

        private static IEnumerable<Octree> PrepOctantsRecursive(AxisAlignedBoundingBox boundingBox, IEnumerable<Face> allFaces, int splitTheshold)
        {
            var octants = new Collection<Octree>();
            var octantBoxes = AabbHelper.PrepareOctants(boundingBox);

            foreach (var octantBox in octantBoxes)
            {
                // TODO: also include faces where none of the corner is contained in the AABB, but the AABB's edges intersect the face triangle!
                var octantFaces = allFaces
                    .Where(f => octantBox.Intersects(f.Triangle))
                    .ToList();

                // If the face-count can no longer be reduced for every octant, we stop. This prevents a stack overflow.
                if (octantFaces.Count() == allFaces.Count())
                {
                    return new Octree[0];
                }

                IEnumerable<Octree> children = new Collection<Octree>();

                if (octantFaces.Count >= splitTheshold)
                {
                    children = PrepOctantsRecursive(octantBox, octantFaces, splitTheshold);
                }

                octants.Add(new Octree(octantBox, octantFaces, children));
            }

            return octants;
        }
    }
}
