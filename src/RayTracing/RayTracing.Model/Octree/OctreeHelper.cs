using RayTracing.Base;
using RayTracing.Math;
using RayTracing.Math.Extensions;
using RayTracing.Model.BoundingBoxes;

namespace RayTracing.Model.Octree
{
    internal static class OctreeHelper
    {
        private static readonly float calcErrorMargin = 0.00001f;

        public static OctreeNode? BuildOctree(IReadOnlyList<Face> allFaces)
        {
            Argument.AssertNotNull(allFaces, nameof(allFaces));

            if (!allFaces.Any())
            {
                return null;
            }

            // Determine the bounding box.
            var aabbTracker = new AabbTracker(allFaces.First().Triangle.CornerA);

            foreach (var face in allFaces)
            {
                aabbTracker.Register(face.Triangle.CornerA);
                aabbTracker.Register(face.Triangle.CornerB);
                aabbTracker.Register(face.Triangle.CornerC);
            }

            var boundingBox = aabbTracker.GetBoundingBox();

            return BuildOctreeInternalRecursive(boundingBox, allFaces);
        }

        private static OctreeNode? BuildOctreeInternalRecursive(AxisAlignedBoundingBox boundingBox, IReadOnlyList<Face> allFaces)
        {
            if (allFaces.Count < 1)
            {
                return new OctreeNode(boundingBox, allFaces, childNodes: null);
            }

            // Find the center.
            var center = boundingBox.Min + (boundingBox.Max - boundingBox.Min).Scale(0.5f);

            // Create child AABBs, and face lists.
            var childBoundingBoxes = CreateChildBoundingBoxes(boundingBox);
            var childFaceLists = childBoundingBoxes.Select(_ => new List<Face>()).ToArray();
            var childGrownAabbTrackers = childBoundingBoxes.Select(aabb => new AabbTracker(aabb.Min)).ToArray();

            // Add each face to one or multiple lists.
            foreach (var face in allFaces)
            {
                for (var i = 0; i < childBoundingBoxes.Length; i++)
                {
                    if (childBoundingBoxes[i].Contains(face.Triangle.CornerA) ||
                        childBoundingBoxes[i].Contains(face.Triangle.CornerB) ||
                        childBoundingBoxes[i].Contains(face.Triangle.CornerB))
                    {
                        childFaceLists[i].Add(face);
                        childGrownAabbTrackers[i].Register(face.Triangle.CornerA);
                        childGrownAabbTrackers[i].Register(face.Triangle.CornerB);
                        childGrownAabbTrackers[i].Register(face.Triangle.CornerC);
                    }
                }
            }

            // Stop when a further subdivision makes no sense anymore.
            if (childFaceLists.Any(l => l.Count >= allFaces.Count))
            {
                return new OctreeNode(boundingBox, allFaces, childNodes: null);
            }

            // Create child nodes.
            var childNodes = new OctreeNode[childBoundingBoxes.Length];

            for (var i = 0; i < childBoundingBoxes.Length; i++)
            {
                childNodes[i] = BuildOctreeInternalRecursive(childGrownAabbTrackers[i].GetBoundingBox(), childFaceLists[i]);
            }

            return new OctreeNode(boundingBox, allFaces, childNodes);
        }

        private static AxisAlignedBoundingBox[] CreateChildBoundingBoxes(AxisAlignedBoundingBox aabb)
        {
            // Find the center offset.
            var centerOffset = (aabb.Max - aabb.Min).Scale(0.5f);

            var leftBottomBack = new AxisAlignedBoundingBox(aabb.Min, aabb.Min + centerOffset);
            var leftBottomFront = leftBottomBack.CopyMove(centerOffset.DirectionZ());
            var leftTopBack = leftBottomBack.CopyMove(centerOffset.DirectionY());
            var leftTopFront = leftTopBack.CopyMove(centerOffset.DirectionZ());

            var rightBottomBack = leftBottomBack.CopyMove(centerOffset.DirectionX());
            var rightBottomFront = rightBottomBack.CopyMove(centerOffset.DirectionZ());
            var rightTopBack = rightBottomBack.CopyMove(centerOffset.DirectionY());
            var rightTopFront = rightTopBack.CopyMove(centerOffset.DirectionZ());

            return new AxisAlignedBoundingBox[]
            {
                leftBottomBack,
                leftBottomFront,
                leftTopBack,
                leftTopFront,
                rightBottomBack,
                rightBottomFront,
                rightTopBack,
                rightTopFront
            };
        }

        private class AabbTracker
        {
            private float minX;
            private float minY;
            private float minZ;

            private float maxX;
            private float maxY;
            private float maxZ;

            public AabbTracker(Vector3 vertex)
            {
                minX = vertex.X;
                minY = vertex.Y;
                minZ = vertex.Z;

                maxX = vertex.X;
                maxY = vertex.Y;
                maxZ = vertex.Z;
            }

            public AxisAlignedBoundingBox GetBoundingBox()
            {
                return new AxisAlignedBoundingBox(
                    new Vector3(
                        minX - calcErrorMargin,
                        minY - calcErrorMargin,
                        minZ - calcErrorMargin),
                    new Vector3(
                        maxX + calcErrorMargin,
                        maxY + calcErrorMargin,
                        maxZ + calcErrorMargin));
            }

            public void Register(Vector3 vertex)
            {
                // Min.
                if (vertex.X < minX)
                {
                    minX = vertex.X;
                }

                if (vertex.Y < minY)
                {
                    minY = vertex.Y;
                }

                if (vertex.Z < minZ)
                {
                    minZ = vertex.Z;
                }

                // Max.
                if (vertex.X > maxX)
                {
                    maxX = vertex.X;
                }

                if (vertex.Y > maxY)
                {
                    maxY = vertex.Y;
                }

                if (vertex.Z > maxZ)
                {
                    maxZ = vertex.Z;
                }
            }
        }
    }
}
