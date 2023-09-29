namespace RayTracing.Math
{
    public struct Line2D
    {
        public Line2D()
        {
            PointA = new Vector2();
            PointB = new Vector2();
        }

        public Line2D(Vector2 pointA, Vector2 pointB)
        {
            PointA = pointA;
            PointB = pointB;
        }

        public Vector2 PointA { get; }
        public Vector2 PointB { get; }

        public float Length()
        {
            return (PointB - PointA).Length();
        }
    }
}
