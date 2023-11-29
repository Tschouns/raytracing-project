
using RayTracing.Math;

namespace RayTracing.Model
{
    /// <summary>
    /// Contains properties specific to spot lights.
    /// </summary>
    public class Spot
    {
        public Spot(Vector3 pointingDirection, float falloffAngle)
        {
            PointingDirection = pointingDirection;
            FalloffAngle = falloffAngle;
        }

        /// <summary>
        /// Gets the spot light pointing direction.
        /// </summary>
        public Vector3 PointingDirection { get; set; }

        /// <summary>
        /// Gets the falloff angle of the spot light.
        /// </summary>
        public float FalloffAngle { get; }
    }
}
