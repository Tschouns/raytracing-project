
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
            var childBoxes = ChildBoundingBoxes(boundingBox);

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

        private static IEnumerable<AxisAlignedBoundingBox> ChildBoundingBoxes(AxisAlignedBoundingBox parent)
        {
            var min = parent.Min;
            var max = parent.Max;

            var c = (max - min) / 2;    // a vector, half way from min to max
            var center = min + c;       // the center of the parent box

            var x = new Vector3(c.X, 0, 0); // the X component of the c vector
            var y = new Vector3(0, c.Y, 0); // the Y component of the c vector
            var z = new Vector3(0, 0, c.Z); // the Z component of the c vector

            var box000 = new AxisAlignedBoundingBox(min, center);
            var box00x = new AxisAlignedBoundingBox(min + x, center + x);
            var box0y0 = new AxisAlignedBoundingBox(min + y, center + y);
            var box0yx = new AxisAlignedBoundingBox(min + x + y, center + x + y);

            var boxZ00 = new AxisAlignedBoundingBox(center - x - y, max - x - y);
            var boxZ0x = new AxisAlignedBoundingBox(center - y, max - y);
            var boxZy0 = new AxisAlignedBoundingBox(center - x, max - x);
            var boxZyx = new AxisAlignedBoundingBox(center, max);

            return new[]
            {
                box000,
                box00x,
                box0y0,
                box0yx,
                boxZ00,
                boxZ0x,
                boxZy0,
                boxZyx,
            };
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
