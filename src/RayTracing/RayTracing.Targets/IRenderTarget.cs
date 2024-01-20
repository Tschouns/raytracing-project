using System.Drawing;

namespace RayTracing.Targets
{
    /// <summary>
    /// Represents a render target which displays an image.
    /// </summary>
    public interface IRenderTarget
    {
        /// <summary>
        /// Sets a pixel at the specified coordinates to the specified color.
        /// </summary>
        void Pixel(int x, int y, Color color);
    }
}
