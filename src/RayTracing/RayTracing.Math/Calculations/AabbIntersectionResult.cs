namespace RayTracing.Math.Calculations
{
    public struct AabbIntersectionResult
    {
        public AabbIntersectionResult(bool doIntersect, float t0, float t1)
        {
            DoIntersect = doIntersect;
            T0 = t0;
            T1 = t1;
        }

        public bool DoIntersect { get; }
        public float T0 { get; }
        public float T1 { get; }

        public static AabbIntersectionResult NoIntersection() => new AabbIntersectionResult(false, 0, 0);
    }
}
