
using RayTracing.Base;
using RayTracing.Math;
using RayTracing.Math.Calculations;
using System.Collections.ObjectModel;

namespace RayTracing.Model.Octrees
{
    public static class OctreeHelper
    {
        public static Octree PrepareOctree(IEnumerable<Face> allFaces, int splitTheshold)
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

            var children = new Collection<Octree>();
            var childBoxes = AabbHelper.PrepareOctants(boundingBox);

            foreach (var childBox in childBoxes)
            {
                var childFaces = allFaces
                    .Where(f => DoesTriangleIntersectBox(f.Triangle, childBox))
                    .ToList();

                children.Add(new Octree(childBox, childFaces, new Octree[0]));
            }

            // TODO: finish implementation.
            return new Octree(boundingBox, allFaces, children);
        }

        private static bool DoesTriangleIntersectBox(Triangle3D triangle, AxisAlignedBoundingBox box)
        {
            return
                IsPointInBox(triangle.CornerA, box) ||
                IsPointInBox(triangle.CornerB, box) ||
                IsPointInBox(triangle.CornerC, box);
        }

        private static bool IsPointInBox(Vector3 point, AxisAlignedBoundingBox box)
        {
            return
                point.X >= box.Min.X && point.X <= box.Max.X &&
                point.Y >= box.Min.Y && point.Y <= box.Max.Y &&
                point.Z >= box.Min.Z && point.Z <= box.Max.Z;
        }
    }
}
