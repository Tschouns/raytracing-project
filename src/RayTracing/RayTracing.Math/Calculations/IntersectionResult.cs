
namespace RayTracing.Math.Calculations
{
    public struct IntersectionResult
    {
        private IntersectionResult(
            bool hasIntersection,
            Vector3? intersectionPoint,
            float? lambda)
        {
            HasIntersection = hasIntersection;
            IntersectionPoint = intersectionPoint;
            Lambda = lambda;
        }

        public bool HasIntersection { get; }
        public Vector3? IntersectionPoint { get; }
        public float? Lambda { get; }

        public static IntersectionResult IntersectAt(
            Vector3 intersectionPoint,
            float lambda)
        {
            return new IntersectionResult(true, intersectionPoint, lambda);
        }

        public static IntersectionResult NoIntersection()
        {
            return new IntersectionResult(false, null, null);
        }
    }
}
