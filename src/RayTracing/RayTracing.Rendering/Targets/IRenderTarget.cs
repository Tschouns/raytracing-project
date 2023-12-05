
using System.Drawing;

namespace RayTracing.Rendering.Targets
{
    /// <summary>
    /// Represents a render target which displays an image.
    /// </summary>
    public interface IRenderTarget
    {
        /// <summary>
        /// Fills the entire render target area with the specified color.
        /// </summary>
        void Fill(Color color);

        /// <summary>
        /// Sets a pixel at the specified coordinates to the specified color.
        /// </summary>
        void SetPixel(int x, int y, Color color);
    }
}
