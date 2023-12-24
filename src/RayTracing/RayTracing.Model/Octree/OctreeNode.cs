
using RayTracing.Base;
using RayTracing.Model.BoundingBoxes;

namespace RayTracing.Model.Octree
{
    public class OctreeNode
    {
        public OctreeNode(
            AxisAlignedBoundingBox boundingBox,
            IReadOnlyList<Face> faces,
            OctreeNode[]? childNodes)
        {
            Argument.AssertNotNull(boundingBox, nameof(boundingBox));
            Argument.AssertNotNull(faces, nameof(faces));

            BoundingBox = boundingBox;
            Volume = CalcVolume(boundingBox);
            Faces = faces;
            ChildNodes = childNodes;
            HasChildren = childNodes != null;
        }

        public AxisAlignedBoundingBox BoundingBox { get; }
        public float Volume { get; }
        public IReadOnlyList<Face> Faces { get; }
        public bool HasChildren { get; }
        public OctreeNode[]? ChildNodes { get; }

        private static float CalcVolume(AxisAlignedBoundingBox aabb)
        {
            var diagonal = aabb.Max - aabb.Min;
            var volume = diagonal.X * diagonal.Y * diagonal.Z;

            return volume;
        }
    }
}
