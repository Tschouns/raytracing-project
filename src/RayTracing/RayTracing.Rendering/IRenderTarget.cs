
using System.Drawing;

namespace RayTracing.Rendering
{
    public interface IRenderTarget
    {
        void SetPixel(int x, int y, Color color);
    }
}
