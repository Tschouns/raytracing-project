namespace RayTracing.Math
{
    public class Line2D
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

        public Line2D(float aX, float aY, float bX, float bY)
            : this(new Vector2(aX, aY), new Vector2(bX, bY))
        {
        }

        public Vector2 PointA { get; set; }
        public Vector2 PointB { get; set; }

        public float Length()
        {
            return (PointB - PointA).Length();
        }
    }
}
