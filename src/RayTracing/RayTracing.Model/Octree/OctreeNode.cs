
using RayTracing.Base;
using RayTracing.Math;
using RayTracing.Math.Calculations;

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
            if (!VectorCalculator3D.DoesRayIntersectWithAabb(rayOrigin, rayDirection, BoundingBox))
            {
                return new List<Face>();
            }

            if (ChildNodes == null)
            {
                // No further subdivision...
                return Faces;
            }

            var faces = ChildNodes
                .Where(c => VectorCalculator3D.DoesRayIntersectWithAabb(rayOrigin, rayDirection, c.BoundingBox))
                .SelectMany(c => c.GetFaces(rayOrigin, rayDirection, minFaceCount))
                .Distinct()
                .ToList();

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
