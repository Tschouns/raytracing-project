using System.Drawing;

namespace RayTracing.CanvasClient
{
    public interface ICanvas
    {
        void Size(int width, int height);
        void Pixel(int x, int y, Color color);
        void Fill(Color color);
        void Quit();
    }
}