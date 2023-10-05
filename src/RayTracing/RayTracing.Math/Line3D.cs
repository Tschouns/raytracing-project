
namespace RayTracing.Math
{
    /// <summary>
    /// Represents a line in 3D space, going through two points A and B.
    /// </summary>
    public class Line3D
    {
        public Line3D()
        {
            this.PointA = new Vector3();
            this.PointB = new Vector3();
        }

        public Line3D(Vector3 pointA, Vector3 pointB)
        {
            this.PointA = pointA;
            this.PointB = pointB;
        }

        public Line3D(
            float aX, float aY, float aZ,
            float bX, float bY, float bZ)
            : this(
                  new Vector3(aX, aY, aZ),
                  new Vector3(bX, bY, bZ))
        {
        }

        public Vector3 PointA { get; set; }
        public Vector3 PointB { get; set; }

        public float Length()
        {
            return (PointB - PointA).Length();
        }
    }
}
