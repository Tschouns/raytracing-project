﻿
namespace RayTracing.Math.Calculations
{
    public static class VectorCalculator
    {
        public static float Determinant(Vector2 u, Vector2 v)
        {
            return u.X * v.Y - u.Y * v.X;
        }

        public static Vector2 Intersect(Line2D line1, Line2D line2)
        {
            var lineVector1 = line1.PointB - line1.PointA;
            var lineVector2 = line2.PointB - line2.PointA;

            var a = lineVector1;
            var b = -lineVector2;
            var c = line2.PointA - line1.PointA;

            var denom = Determinant(a, b);

            var x = Determinant(c, b) / denom;
            var y = Determinant(a, c) / denom;

            return new Vector2(x, y);
        }
    }
}