
using RayTracing.Base;
using RayTracing.Math;

namespace RayTracing.Model.Octree
{
    public class OctreeNode
    {
        public OctreeNode(
            AxisAlignedBoundingBox boundingBox,
            IEnumerable<Face> faces,
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
            Faces = faces;
            Child1 = child1;
            Child2 = child2;
            Child3 = child3;
            Child4 = child4;
            Child5 = child5;
            Child6 = child6;
            Child7 = child7;
            Child8 = child8;
        }

        public AxisAlignedBoundingBox BoundingBox { get; }
        public IEnumerable<Face> Faces { get; }
        public OctreeNode? Child1 { get; }
        public OctreeNode? Child2 { get; }
        public OctreeNode? Child3 { get; }
        public OctreeNode? Child4 { get; }
        public OctreeNode? Child5 { get; }
        public OctreeNode? Child6 { get; }
        public OctreeNode? Child7 { get; }
        public OctreeNode? Child8 { get; }
    }
}
