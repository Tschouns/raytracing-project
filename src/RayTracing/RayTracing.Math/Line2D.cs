namespace RayTracing.Math
{
    /// <summary>
    /// Represents a line in 2D space, going through two points A and B.
    /// </summary>
    public class Line2D
    {
        public Line2D(Vector2 pointA, Vector2 pointB)
        {
            this.PointA = pointA;
            this.PointB = pointB;
        }

        public Line2D(float aX, float aY, float bX, float bY)
            : this(new Vector2(aX, aY), new Vector2(bX, bY))
        {
        }

        public Vector2 PointA { get; set; }
        public Vector2 PointB { get; set; }

        public float Length()
        {
            return (this.PointB - this.PointA).Length();
        }
    }
}
