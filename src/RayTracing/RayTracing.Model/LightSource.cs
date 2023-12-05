
using RayTracing.Math;
using System.Drawing;

namespace RayTracing.Model
{
    /// <summary>
    /// Represents a light source in 
    /// </summary>
    public class LightSource
    {
        public LightSource(Vector3 location, Color color, Spot? spot)
        {
            Location = location;
            Color = color;
            Spot = spot;
        }

        public Vector3 Location { get; }
        public Color Color { get; }
        public Spot? Spot { get; }
    }
}
