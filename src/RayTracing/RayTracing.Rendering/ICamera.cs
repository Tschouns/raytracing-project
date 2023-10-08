
namespace RayTracing.Rendering
{
    /// <summary>
    /// Represents a camera.
    /// </summary>
    public interface ICamera
    {
        /// <summary>
        /// Produces the rays, associated with pixel coordinates, based on the camera's settings.
        /// </summary>
        IEnumerable<PixelRay> GeneratePixelRays();
    }
}
