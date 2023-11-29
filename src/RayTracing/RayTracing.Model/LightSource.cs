
using RayTracing.Base;
using RayTracing.Math;
using System.Drawing;

namespace RayTracing.Model
{
    /// <summary>
    /// Represents a light source in a scene.
    /// </summary>
    public class LightSource
    {
        public LightSource(string name, Vector3 location, Color color, Spot? spot)
        {
            Argument.AssertNotNull(name, nameof(name));

            Name = name;
            Location = location;
            Color = color;
            Spot = spot;
        }

        /// <summary>
        /// Gets the geometry name (mainly for debugging purposes).
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the light source location.
        /// </summary>
        public Vector3 Location { get; }

        /// <summary>
        /// Gets the color of the light.
        /// </summary>
        public Color Color { get; }

        /// <summary>
        /// Gets the spot properties, if it's a spot light; otherwise null.
        /// </summary>
        public Spot? Spot { get; }
    }
}
