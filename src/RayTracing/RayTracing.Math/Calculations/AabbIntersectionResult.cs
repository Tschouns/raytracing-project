namespace RayTracing.Math.Calculations
{
    public struct AabbIntersectionResult
    {
        public AabbIntersectionResult(bool doIntersect, float t0, float t1)
        {
            this.DoIntersect = doIntersect;
            this.T0 = t0;
            this.T1 = t1;
        }

        public bool DoIntersect { get; }
        public float T0 { get; }
        public float T1 { get; }

        public static AabbIntersectionResult NoIntersection()
        {
            return new AabbIntersectionResult(false, 0, 0);
        }
    }
}
