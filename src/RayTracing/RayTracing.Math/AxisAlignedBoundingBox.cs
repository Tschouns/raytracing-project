namespace RayTracing.Math
{
    /// <summary>
    /// Represents an axis-aligned bounding box.
    /// </summary>
    public class AxisAlignedBoundingBox
    {
        public AxisAlignedBoundingBox(Vector3 min, Vector3 max)
        {
            Min = min;
            Max = max;
        }

        public Vector3 Min;
        public Vector3 Max;
    }
}
