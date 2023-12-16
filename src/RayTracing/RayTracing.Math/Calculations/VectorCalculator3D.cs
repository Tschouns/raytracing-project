
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

        public static bool IntersectRayWithAabb(Vector3 origin, Vector3 direction, AxisAlignedBoundingBox aabb)
        {
            var pos = AsArray(origin);
            var dir = AsArray(direction);
            var aabbMin = AsArray(aabb.Min);
            var aabbMax = AsArray(aabb.Max);

            for (var i = 0; i < 3; i++)
            {
                var invDir = 1.0f / dir[i];
                var t0 = (aabbMin[i] - pos[i]) * invDir;
                var t1 = (aabbMax[i] - pos[i]) * invDir;

                if (invDir < 0f)
                {
                    var temp = t1;
                    t1 = t0;
                    t0 = temp;
                }

                if (t1 <= t0)
                {
                    return false;
                }
            }

            return true;
        }

        private static float[] AsArray(Vector3 vec)
        {
            return new float[] { vec.X, vec.Y, vec.Z };
        }
    }
}
