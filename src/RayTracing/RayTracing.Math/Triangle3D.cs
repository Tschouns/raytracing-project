
namespace RayTracing.Math
{
    /// <summary>
    /// Represents a triangle in 3D space, specifying three corners A, B, and C.
    /// </summary>
    public class Triangle3D
    {
        public Triangle3D()
        {
            this.PointA = new Vector3();
            this.PointB = new Vector3();
            this.PointC = new Vector3();
        }

        public Triangle3D(Vector3 pointA, Vector3 pointB, Vector3 pointC)
        {
            this.PointA = pointA;
            this.PointB = pointB;
            this.PointC = pointC;
        }

        public Triangle3D(
            float aX, float aY, float aZ,
            float bX, float bY, float bZ,
            float cX, float cY, float cZ)
            : this(
                  new Vector3(aX, aY, aZ),
                  new Vector3(bX, bY, bZ),
                  new Vector3(cX, cY, cZ))
        {
        }

        public Vector3 PointA { get; set; }
        public Vector3 PointB { get; set; }
        public Vector3 PointC { get; set; }
    }
}
