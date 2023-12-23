
using RayTracing.Base;
using RayTracing.Math;
using RayTracing.Math.Calculations;

namespace RayTracing.Model.Octree
{
    public class OctreeNode
    {
        private readonly OctreeNode[] childNodes;
        public OctreeNode(
            AxisAlignedBoundingBox boundingBox,
            IReadOnlyList<Face> faces,
            OctreeNode? child1,
            OctreeNode? child2,
            OctreeNode? child3,
            OctreeNode? child4,
            OctreeNode? child5,
            OctreeNode? child6,
            OctreeNode? child7,
            OctreeNode? child8)
        {
            Argument.AssertNotNull(boundingBox, nameof(boundingBox));
            Argument.AssertNotNull(faces, nameof(faces));

            BoundingBox = boundingBox;
            Volume = CalcVolume(boundingBox);
            Faces = faces;

            Child1 = child1;
            Child2 = child2;
            Child3 = child3;
            Child4 = child4;
            Child5 = child5;
            Child6 = child6;
            Child7 = child7;
            Child8 = child8;

            childNodes = new[] { Child1, Child2, Child3, Child4, Child5, Child6, Child7, Child8 }
                .Where(c => c != null)
                .ToArray();
        }

        public AxisAlignedBoundingBox BoundingBox { get; }
        public float Volume { get; }
        public IReadOnlyList<Face> Faces { get; }
        public OctreeNode? Child1 { get; }
        public OctreeNode? Child2 { get; }
        public OctreeNode? Child3 { get; }
        public OctreeNode? Child4 { get; }
        public OctreeNode? Child5 { get; }
        public OctreeNode? Child6 { get; }
        public OctreeNode? Child7 { get; }
        public OctreeNode? Child8 { get; }

        public IEnumerable<Face> PruneFacesForRayTest(Vector3 rayOrigin, Vector3 rayDirection, int minFaceCount)
        {
            if (!VectorCalculator3D.DoesRayIntersectWithAabb(rayOrigin, rayDirection, BoundingBox))
            {
                return new List<Face>();
            }

            if (Faces.Count <= minFaceCount)
            {
                return Faces;
            }

            var prunedChildFaces = childNodes
                .SelectMany(c => c.PruneFacesForRayTest(rayOrigin, rayDirection, minFaceCount))
                .ToList(); // Debugging

            return prunedChildFaces;
        }

        private static float CalcVolume(AxisAlignedBoundingBox aabb)
        {
            var diagonal = aabb.Max - aabb.Min;
            var volume = diagonal.X * diagonal.Y * diagonal.Z;

            return volume;
        }
    }
}
