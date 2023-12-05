
namespace RayTracing.Model
{
    public class Spot
    {
        public Spot(float falloffAngle)
        {
            FalloffAngle = falloffAngle;
        }

        public float FalloffAngle { get; }
    }
}
