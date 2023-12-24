
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
        public static AabbIntersectionResult DoesRayIntersectWithAabb(Vector3 rayOrigin, Vector3 rayDirection, Vector3 aabbMin, Vector3 aabbMax)
        {
            var tMin = aabbMin - rayOrigin;
            var tMax = aabbMax - rayOrigin;

            var doIntersect = true;

            // X
            var incDirX = 1 / rayDirection.X;
            var t0X = tMin.X * incDirX;
            var t1X = tMax.X * incDirX;

            if (t1X <= t0X ^ incDirX < 0)
            {
                doIntersect = false;
            }

            // Y
            var incDirY = 1 / rayDirection.Y;
            var t0Y = tMin.Y * incDirY;
            var t1Y = tMax.Y * incDirY;

            if (t1Y <= t0Y ^ incDirY < 0)
            {
                doIntersect = false;
            }

            // Z
            var incDirZ = 1 / rayDirection.Z;
            var t0Z = tMin.Z * incDirZ;
            var t1Z = tMax.Z * incDirZ;

            if (t1Z <= t0Z ^ incDirZ < 0)
            {
                doIntersect = false;
            }

            var t0 = new Vector3(t0X, t0Y, t0Z);
            var t1 = new Vector3(t1X, t1Y, t1Z);

            return new AabbIntersectionResult(
                doIntersect,
                t0.Dot(rayDirection),
                t1.Dot(rayDirection));
        }

        private static float[] AsArray(Vector3 vec)
        {
            return new float[] { vec.X, vec.Y, vec.Z };
        }

        private static Vector3 Min(Vector3 a, Vector3 b)
        {
            if (a.LengthSquared() < b.LengthSquared())
            {
                return a;
            }
            else
            {
                return b;
            }
        }

        private static Vector3 Max(Vector3 a, Vector3 b)
        {
            if (a.LengthSquared() > b.LengthSquared())
            {
                return a;
            }
            else
            {
                return b;
            }
        }
    }
}
