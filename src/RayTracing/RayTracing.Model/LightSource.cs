
using RayTracing.Base;
using RayTracing.Math;

namespace RayTracing.Model
{
    /// <summary>
    /// Represents a light source in a scene.
    /// </summary>
    public class LightSource
    {
        public LightSource(string name, Vector3 location, ArgbColor color, Spot? spot)
        {
            Argument.AssertNotNull(name, nameof(name));

            this.Name = name;
            this.Location = location;
            this.Color = color;
            this.Spot = spot;
        }

        /// <summary>
        /// Gets the geometry name (mainly for debugging purposes).
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the light source location.
        /// </summary>
        public Vector3 Location { get; set; }

        /// <summary>
        /// Gets the color of the light.
        /// </summary>
        public ArgbColor Color { get; set; }

        /// <summary>
        /// Gets the spot properties, if it's a spot light; otherwise null.
        /// </summary>
        public Spot? Spot { get; }
    }
}
