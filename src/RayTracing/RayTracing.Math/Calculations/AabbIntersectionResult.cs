namespace RayTracing.Math.Calculations
{
    public struct AabbIntersectionResult
    {
        public AabbIntersectionResult(bool doIntersect, Vector3 t0, Vector3 t1)
        {
            DoIntersect = doIntersect;
            T0 = t0;
            T1 = t1;
        }

        public bool DoIntersect { get; }
        public Vector3 T0 { get; }
        public Vector3 T1 { get; }
    }
}
