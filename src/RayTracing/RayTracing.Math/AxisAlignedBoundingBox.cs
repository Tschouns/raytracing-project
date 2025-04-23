namespace RayTracing.Math
{
    /// <summary>
    /// Represents an axis-aligned bounding box.
    /// </summary>
    public class AxisAlignedBoundingBox
    {
        public AxisAlignedBoundingBox(Vector3 min, Vector3 max)
        {
            if (min.X > max.X ||
                min.Y > max.Y ||
                min.Z > max.Z)
            {
                throw new ArgumentException($"The min ({min}) and max ({max}) arguments must contain the lowest and greatest values along all axes respectively.");
            }

            Min = min;
            Max = max;
        }

        public Vector3 Min;
        public Vector3 Max;

        public bool Contains(Vector3 point)
        {
            return
                point.X >= this.Min.X && point.X <= this.Max.X &&
                point.Y >= this.Min.Y && point.Y <= this.Max.Y &&
                point.Z >= this.Min.Z && point.Z <= this.Max.Z;
        }

        public bool Contains(Vector3 point, float t)
        {
            return
                point.X > (this.Min.X - t) && point.X < (this.Max.X + t) &&
                point.Y > (this.Min.Y - t) && point.Y < (this.Max.Y + t) &&
                point.Z > (this.Min.Z - t) && point.Z < (this.Max.Z + t);
        }
    }
}
