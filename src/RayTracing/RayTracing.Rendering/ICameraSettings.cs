
using RayTracing.Math;

namespace RayTracing.Rendering
{
    /// <summary>
    /// Specifies the camera's variable settings.
    /// </summary>
    public interface ICameraSettings
    {
        /// <summary>
        /// Gets or sets the camera's focal length.
        /// </summary>
        public float FocalLength { get; set; }

        /// <summary>
        /// Gets or sets the camera position.
        /// </summary>
        public Vector3 Position { get; set; }

        /// <summary>
        /// Gets or sets the camera's looking direction.
        /// </summary>
        public Vector3 LookingDirection { get; set; }
    }
}
