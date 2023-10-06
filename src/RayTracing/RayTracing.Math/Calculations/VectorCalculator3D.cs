
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
        public static Vector3? Intersect(Line3D line, Plane3D plane)
        {
            Argument.AssertNotNull(line, nameof(line));
            Argument.AssertNotNull(plane, nameof(plane));

            return Intersect(
                line.PointA,
                line.PointB - line.PointA,
                plane.PointP,
                plane.VectorU,
                plane.VectorV);
        }

        /// <summary>
        /// Calculates the intersection between a specified line with a specified plane, represented
        /// as follows:
        /// Line: r = p + lambda * u
        /// Plane: r = q + mu * v + nu * w
        /// </summary>
        public static Vector3? Intersect(Vector3 p, Vector3 u, Vector3 q, Vector3 v, Vector3 w)
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
    }
}
