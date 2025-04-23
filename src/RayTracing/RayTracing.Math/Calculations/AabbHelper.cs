
using RayTracing.Base;

namespace RayTracing.Math.Calculations
{
    public static class AabbHelper
    {
        /// <summary>
        /// "Cuts" the specified parent box in half along each axis, and creates child boxes -- i.e. octants.
        /// </summary>
        /// <param name="parent">
        /// The parent box
        /// </param>
        /// <returns>
        /// The child octants
        /// </returns>
        public static IEnumerable<AxisAlignedBoundingBox> PrepareOctants(AxisAlignedBoundingBox parent)
        {
            Argument.AssertNotNull(parent, nameof(parent));

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

        /// <summary>
        /// Checks whether a ray -- specified by an origin and direction vector -- intersects with the specified
        /// axis-aligned bounding box.
        /// </summary>
        /// <param name="rayOrigin">
        /// The ray origin vector
        /// </param>
        /// <param name="rayDirection">
        /// The ray direction vector
        /// </param>
        /// <param name="box">
        /// The axis-aligned bounding box
        /// </param>
        /// <returns>
        /// A value indicating whether the ray intersects with the bounding box
        /// </returns>
        public static AabbIntersectionResult IntersectAabb(Vector3 rayOrigin, Vector3 rayDirection, AxisAlignedBoundingBox box)
        {
            return IntersectAabb(rayOrigin, rayDirection, box.Min, box.Max);
        }

        /// <summary>
        /// Checks whether a ray -- specified by an origin and direction vector -- intersects with the specified
        /// axis-aligned bounding box.
        /// </summary>
        /// <param name="rayOrigin">
        /// The ray origin vector
        /// </param>
        /// <param name="rayDirection">
        /// The ray direction vector
        /// </param>
        /// <param name="boxMin">
        /// The axis-aligned bounding box minimum
        /// </param>
        /// <param name="boxMax">
        /// The axis-aligned bounding box maximum
        /// </param>
        /// <returns>
        /// A value indicating whether the ray intersects with the bounding box
        /// </returns>
        public static AabbIntersectionResult IntersectAabb(Vector3 rayOrigin, Vector3 rayDirection, Vector3 boxMin, Vector3 boxMax)
        {
            var tMin = float.MinValue;
            var tMax = float.MaxValue;

            for (var i = 0; i < 3; i++)
            {
                if (System.Math.Abs(rayDirection.AsArray()[i]) < 1e-8)
                {
                    // Ray is parallel to slab. No hit if origin not within slab
                    if (rayOrigin.AsArray()[i] < boxMin.AsArray()[i] || rayOrigin.AsArray()[i] > boxMax.AsArray()[i])
                    {
                        return AabbIntersectionResult.NoIntersection();
                    }
                }
                else
                {
                    var invD = 1.0f / rayDirection.AsArray()[i];
                    var t0 = (boxMin.AsArray()[i] - rayOrigin.AsArray()[i]) * invD;
                    var t1 = (boxMax.AsArray()[i] - rayOrigin.AsArray()[i]) * invD;

                    if (t0 > t1)
                    {
                        var temp = t0;
                        t0 = t1;
                        t1 = temp;
                    }

                    tMin = System.Math.Max(tMin, t0);
                    tMax = System.Math.Min(tMax, t1);

                    if (tMax < tMin)
                    {
                        return AabbIntersectionResult.NoIntersection();
                    }
                }
            }

            return new AabbIntersectionResult(true, tMin, tMax);
        }

        private static float[] AsArray(this Vector3 vec)
        {
            return new float[] { vec.X, vec.Y, vec.Z };
        }
    }
}
