
using RayTracing.Base;

namespace RayTracing.Math.Calculations
{
    public static class VectorCalculator3D
    {
        public static float Determinant(Vector3 u, Vector3 v, Vector3 w)
        {
            return (u.X * v.Y * w.Z)
                 + (u.Y * v.Z * w.X)
                 + (u.Z * v.X * w.Y)
                 - (u.Z * v.Y * w.X)
                 - (u.X * v.Z * w.Y)
                 - (u.Y * v.X * w.Z);
        }

        /// <summary>
        /// Calculates the intersection between a specified line with a specified plane.
        /// </summary>
        public static Vector3? IntersectPlane(Line3D line, Plane3D plane)
        {
            Argument.AssertNotNull(line, nameof(line));
            Argument.AssertNotNull(plane, nameof(plane));

            return IntersectPlane(
                line.PointA,
                line.PointB - line.PointA,
                plane.PointP,
                plane.VectorU,
                plane.VectorV);
        }

        /// <summary>
        /// Calculates the intersection between a specified line with a specified triangle.
        /// </summary>
        public static IntersectionResult IntersectTriangle(Line3D line, Triangle3D triangle)
        {
            Argument.AssertNotNull(line, nameof(line));
            Argument.AssertNotNull(triangle, nameof(triangle));

            return IntersectTriangle(
                line.PointA,
                line.PointB - line.PointA,
                triangle.CornerA,
                triangle.CornerB - triangle.CornerA,
                triangle.CornerC - triangle.CornerA);
        }

        /// <summary>
        /// Calculates the intersection between a specified line with a specified plane, represented
        /// as follows:
        /// Line: r = p + lambda * u
        /// Plane: r = q + mu * v + nu * w
        /// </summary>
        public static Vector3? IntersectPlane(Vector3 p, Vector3 u, Vector3 q, Vector3 v, Vector3 w)
        {
            // lambda * u - mu * v - nu * w = q - p

            var d = Determinant(u, -v, -w);
            if (d == 0)
            {
                return null;
            }

            var lambda = Determinant(q - p, -v, -w) / d;
            var intersection = p + lambda * u;

            return intersection;
        }

        /// <summary>
        /// Calculates the intersection between a specified line with a specified triangle area, represented
        /// as follows:
        /// Line: r = p + lambda * u
        /// Triangle: corner point q, edge vector v, edge vector w
        /// </summary>
        public static IntersectionResult IntersectTriangle(Vector3 p, Vector3 u, Vector3 q, Vector3 v, Vector3 w)
        {
            // lambda * u - mu * v - nu * w = q - p

            var d = Determinant(u, -v, -w);
            if (d == 0)
            {
                return IntersectionResult.NoIntersection();
            }

            var qp = q - p;
            var mu = Determinant(u, qp, -w) / d;
            if (mu < 0)
            {
                return IntersectionResult.NoIntersection();
            }

            var nu = Determinant(u, -v, qp) / d;
            if (nu < 0)
            {
                return IntersectionResult.NoIntersection();
            }

            if (mu + nu > 1)
            {
                return IntersectionResult.NoIntersection();
            }

            var lambda = Determinant(qp, -v, -w) / d;
            var intersection = p + lambda * u;

            return IntersectionResult.IntersectAt(intersection, lambda);
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
        /// <param name="aabbMin">
        /// The axis-aligned bounding box minimum
        /// </param>
        /// <param name="aabbMax">
        /// The axis-aligned bounding box maximum
        /// </param>
        /// <returns>
        /// A value indicating whether the ray intersects with the bounding box
        /// </returns>
        public static AabbIntersectionResult IntersectAabb(Vector3 rayOrigin, Vector3 rayDirection, Vector3 boxMin, Vector3 boxMax)
        {
            var tMin = float.MinValue;
            var tMax = float.MaxValue;

            for (int i = 0; i < 3; i++)
            {
                if (System.Math.Abs(rayDirection.AsArray()[i]) < 1e-8)
                {
                    // Ray is parallel to slab. No hit if origin not within slab
                    if (rayOrigin.AsArray()[i] < boxMin.AsArray()[i] || rayOrigin.AsArray()[i] > boxMax.AsArray()[i])
                        return AabbIntersectionResult.NoIntersection();
                }
                else
                {
                    float invD = 1.0f / rayDirection.AsArray()[i];
                    float t0 = (boxMin.AsArray()[i] - rayOrigin.AsArray()[i]) * invD;
                    float t1 = (boxMax.AsArray()[i] - rayOrigin.AsArray()[i]) * invD;

                    if (t0 > t1)
                    {
                        float temp = t0;
                        t0 = t1;
                        t1 = temp;
                    }

                    tMin = System.Math.Max(tMin, t0);
                    tMax = System.Math.Min(tMax, t1);

                    if (tMax < tMin)
                        return AabbIntersectionResult.NoIntersection();
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
