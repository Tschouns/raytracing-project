
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
            return Intersect(
                line1.PointA,
                line1.PointB - line1.PointA,
                line2.PointA,
                line2.PointB - line2.PointA);
        }

        /// <summary>
        /// Calculates the intersection of two lines, represented as:
        /// A: r = p + lambda * u
        /// B: r = q + mu * v
        /// </summary>
        public static Vector2? Intersect(Vector2 p, Vector2 u, Vector2 q, Vector2 v)
        {
            var denom = Determinant(u, -v);
            if (denom == 0)
            {
                return null;
            }

            var r = q - p;
            var lambda = Determinant(r, -v) / denom;
            var intersection = p + lambda * u;

            return intersection;
        }
    }
}
