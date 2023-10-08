
using RayTracing.Math;

namespace RayTracing.Rendering
{
    /// <summary>
    /// Provides a method to render scene content to a render target based on a camera. 
    /// </summary>
    public interface IRender
    {
        void Render(IEnumerable<Triangle3D> triangles, ICamera camera, IRenderTarget target);
    }
}
