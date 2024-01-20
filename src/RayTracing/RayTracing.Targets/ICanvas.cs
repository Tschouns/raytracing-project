using System.Drawing;

namespace RayTracing.Targets
{
    public interface ICanvas
    {
        IRenderTarget Draw { get; }

        void Size(int width, int height);

        /// <summary>
        /// Fills the entire render target area with the specified color.
        /// </summary>
        void Fill(Color color);
    }
}