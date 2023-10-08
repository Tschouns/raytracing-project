
using RayTracing.Math;

namespace RayTracing.Rendering
{
    /// <summary>
    /// Exposes a camera's fixed intrinsic properties.
    /// </summary>
    public interface ICameraFixedProperties
    {
        /// <summary>
        /// Gets the camera's size in the world.
        /// </summary>
        public Vector2 Size { get; }

        /// <summary>
        /// Gets the camera's horizontal resolution.
        /// </summary>
        public ushort HorizontalResolution { get; }

        /// <summary>
        /// Gets the camera's vertical resolution.
        /// </summary>
        public ushort VerticalResolution { get; }
    }
}
