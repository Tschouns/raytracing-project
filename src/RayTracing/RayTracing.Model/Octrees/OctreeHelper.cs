
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

            var children = PrepOctantsRecursive(boundingBox, allFaces, splitTheshold: 100);

            return new Octree(boundingBox, allFaces, children);
        }

        private static IEnumerable<Octree> PrepOctantsRecursive(AxisAlignedBoundingBox boundingBox, IEnumerable<Face> allFaces, int splitTheshold)
        {
            var octants = new Collection<Octree>();
            var octantBoxes = AabbHelper.PrepareOctants(boundingBox);

            foreach (var octantBox in octantBoxes)
            {
                var octantFaces = allFaces
                    .Where(f => DoesTriangleIntersectBox(f.Triangle, octantBox))
                    .ToList();

                IEnumerable<Octree> children = new Collection<Octree>();

                if (octantFaces.Count >= splitTheshold)
                {
                    children = PrepOctantsRecursive(octantBox, octantFaces, splitTheshold);
                }

                octants.Add(new Octree(octantBox, octantFaces, new Octree[0]));
            }

            return octants;
        }

        private static bool DoesTriangleIntersectBox(Triangle3D triangle, AxisAlignedBoundingBox box)
        {
            return
                box.Contains(triangle.CornerA) ||
                box.Contains(triangle.CornerB) ||
                box.Contains(triangle.CornerC);
        }
    }
}
