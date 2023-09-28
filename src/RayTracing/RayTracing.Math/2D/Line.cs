
namespace RayTracing.Math._2D
{
    public struct Line
    {
        public Line()
        {
            this.PointA = new Vector2();
            this.PointB = new Vector2();
        }

        public Line(Vector2 pointA, Vector2 pointB)
        {
            this.PointA = pointA;
            this.PointB = pointB;
        }

        public Vector2 PointA { get; }
        public Vector2 PointB { get; }

        public float Length()
        {
            return (this.PointB - this.PointA).Length();
        }
    }
}
