
using RayTracing.Base;
using RayTracing.Math;
using RayTracing.Math.Calculations;
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
        }

        public AxisAlignedBoundingBox BoundingBox { get; }
        public float Volume { get; }
        public IReadOnlyList<Face> Faces { get; }
        public OctreeNode[]? ChildNodes { get; }

        public IEnumerable<Face> GetFaces(Vector3 rayOrigin, Vector3 rayDirection, int minFaceCount)
        {
            var rayAndAabb = VectorCalculator3D.DoesRayIntersectWithAabb(rayOrigin, rayDirection, BoundingBox.Min, BoundingBox.Max);

            if (!rayAndAabb.DoIntersect)
            {
                return new Face[0];
            }

            if (ChildNodes == null)
            {
                // No further subdivision...
                return Faces;
            }

            var nearestRelevantChildNode = ChildNodes
                .Select(c => new { Node = c, Result = VectorCalculator3D.DoesRayIntersectWithAabb(rayOrigin, rayDirection, c.BoundingBox.Min, c.BoundingBox.Max) })
                .Where(c => c.Result.DoIntersect)
                .OrderBy(c => c.Result.T0.LengthSquared())
                .FirstOrDefault();

            if (nearestRelevantChildNode == null)
            {
                return new Face[0];
            }

            var faces = nearestRelevantChildNode.Node.GetFaces(rayOrigin, rayDirection, minFaceCount);

            return faces;
        }

        private static float CalcVolume(AxisAlignedBoundingBox aabb)
        {
            var diagonal = aabb.Max - aabb.Min;
            var volume = diagonal.X * diagonal.Y * diagonal.Z;

            return volume;
        }
    }
}
