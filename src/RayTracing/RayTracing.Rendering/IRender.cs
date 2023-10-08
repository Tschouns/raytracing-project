
using RayTracing.Math;

namespace RayTracing.Rendering
{
    /// <summary>
    /// Provides a method to render scene content to a render target based on a camera. 
    /// </summary>
    public interface IRender
    {
        /// <summary>
        /// Renders the specified content (triangles) to the specified render target based on the specified camera.
        /// </summary>
        void Render(IEnumerable<Triangle3D> triangles, ICamera camera, IRenderTarget target);
    }
}
