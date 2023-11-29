using RayTracing.Model;
using RayTracing.Rendering.Cameras;
using RayTracing.Rendering.Settings;
using RayTracing.Rendering.Targets;

namespace RayTracing.Rendering
{
    /// <summary>
    /// Provides a method to render scene content to a render target based on a camera. 
    /// </summary>
    public interface IRender
    {
        /// <summary>
        /// Renders the specified scene to the specified render target based on the specified camera.
        /// </summary>
        void Render(Scene scene, ICamera camera, IRenderTarget target, IRenderSettings settings);
    }
}
