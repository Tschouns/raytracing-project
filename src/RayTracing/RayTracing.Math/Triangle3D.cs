
namespace RayTracing.Math
{
    /// <summary>
    /// Represents a triangle in 3D space, specifying three corners A, B, and C.
    /// </summary>
    public class Triangle3D
    {
        public Triangle3D()
        {
            this.CornerA = new Vector3();
            this.CornerB = new Vector3();
            this.CornerC = new Vector3();
        }

        public Triangle3D(Vector3 cornerA, Vector3 cornerB, Vector3 cornerC)
        {
            this.CornerA = cornerA;
            this.CornerB = cornerB;
            this.CornerC = cornerC;
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

        public Vector3 CornerA { get; set; }
        public Vector3 CornerB { get; set; }
        public Vector3 CornerC { get; set; }
    }
}
