
using RayTracing.Math;

namespace RayTracing.Model
{
    public class Spot
    {
        public Spot(Vector3 pointingDirection, float falloffAngle)
        {
            PointingDirection = pointingDirection;
            FalloffAngle = falloffAngle;
        }

        public Vector3 PointingDirection { get; set; }
        public float FalloffAngle { get; }
    }
}
