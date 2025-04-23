
namespace RayTracing.Math.Calculations
{
    public static class AabbHelper
    {
        public static IEnumerable<AxisAlignedBoundingBox> PrepareOctants(AxisAlignedBoundingBox parent)
        {
            var min = parent.Min;
            var max = parent.Max;

            var c = (max - min) / 2;    // a vector, half way from min to max
            var center = min + c;       // the center of the parent box

            var x = new Vector3(c.X, 0, 0); // the X component of the c vector
            var y = new Vector3(0, c.Y, 0); // the Y component of the c vector
            var z = new Vector3(0, 0, c.Z); // the Z component of the c vector

            var box000 = new AxisAlignedBoundingBox(min, center);
            var box00x = new AxisAlignedBoundingBox(min + x, center + x);
            var box0y0 = new AxisAlignedBoundingBox(min + y, center + y);
            var box0yx = new AxisAlignedBoundingBox(min + x + y, center + x + y);

            var boxZ00 = new AxisAlignedBoundingBox(center - x - y, max - x - y);
            var boxZ0x = new AxisAlignedBoundingBox(center - y, max - y);
            var boxZy0 = new AxisAlignedBoundingBox(center - x, max - x);
            var boxZyx = new AxisAlignedBoundingBox(center, max);

            return new[]
            {
                box000,
                box00x,
                box0y0,
                box0yx,
                boxZ00,
                boxZ0x,
                boxZy0,
                boxZyx,
            };
        }
    }
}
