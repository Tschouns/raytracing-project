
namespace RayTracing.Math
{
    /// <summary>
    /// Represents a plane in 3D space, specifying an anchor point P and two vectors U and V.
    /// </summary>
    public class Plane3D
    {
        public Plane3D()
        {
            this.PointP = new Vector3();
            this.VectorU = new Vector3();
            this.VectorV = new Vector3();
        }

        public Plane3D(Vector3 pointP, Vector3 vectorU, Vector3 vectorV)
        {
            this.PointP = pointP;
            this.VectorU = vectorU;
            this.VectorV = vectorV;
        }

        public Plane3D(
            float pX, float pY, float pZ,
            float uX, float uY, float uZ,
            float vX, float vY, float vZ)
            : this(
                  new Vector3(pX, pY, pZ),
                  new Vector3(uX, uY, uZ),
                  new Vector3(vX, vY, vZ))
        {
        }

        public Vector3 PointP { get; set; }
        public Vector3 VectorU { get; set; }
        public Vector3 VectorV { get; set; }
    }
}
