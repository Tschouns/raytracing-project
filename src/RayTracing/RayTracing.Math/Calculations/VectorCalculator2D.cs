
using RayTracing.Base;

namespace RayTracing.Math.Calculations
{
    public static class VectorCalculator2D
    {
        public static float Determinant(Vector2 u, Vector2 v)
        {
            return (u.X * v.Y) - (u.Y * v.X);
        }

        /// <summary>
        /// Calculates the intersection of the two specified lines.
        /// </summary>
        public static Vector2? Intersect(Line2D line1, Line2D line2)
        {
            Argument.AssertNotNull(line1, nameof(line1));
            Argument.AssertNotNull(line2, nameof(line2));

            return Intersect(
                line1.PointA,
                line1.PointB - line1.PointA,
                line2.PointA,
                line2.PointB - line2.PointA);
        }

        /// <summary>
        /// Calculates the intersection of two lines, represented as:
        /// 1: r = p + lambda * u
        /// 2: r = q + mu * v
        /// </summary>
        public static Vector2? Intersect(Vector2 p, Vector2 u, Vector2 q, Vector2 v)
        {
            var d = Determinant(u, -v);
            if (d == 0)
            {
                return null;
            }

            var lambda = Determinant(q - p, -v) / d;
            var intersection = p + lambda * u;

            return intersection;
        }
    }
}
