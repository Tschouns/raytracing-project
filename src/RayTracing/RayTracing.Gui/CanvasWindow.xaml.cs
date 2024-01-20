using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RayTracing.Gui
{
    /// <summary>
    /// Interaction logic for CanvasWindow.xaml
    /// </summary>
    public partial class CanvasWindow : Window, ICanvas
    {
        private int? width;
        private int? height;
        private WriteableBitmap? bitmap;

        public CanvasWindow()
        {
            InitializeComponent();
            //RenderOptions.SetBitmapScalingMode(image, BitmapScalingMode.NearestNeighbor);
            //RenderOptions.SetEdgeMode(image, EdgeMode.Aliased);
        }

        public void Size(int width, int height)
        {
            this.width = width;
            this.height = height;

            var newBitmap = new WriteableBitmap(width, height, 96, 96, PixelFormats.Bgra32, null);
            bitmap = newBitmap;

            image.Source = bitmap;

            image.Stretch = Stretch.None;
            image.HorizontalAlignment = HorizontalAlignment.Center;
            image.VerticalAlignment = VerticalAlignment.Center;
        }

        public void Fill(System.Drawing.Color color)
        {
            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    Pixel(i, j, color);
                }
            }
        }

        public void Pixel(int x, int y, System.Drawing.Color color)
        {
            if (bitmap == null ||
                x >= width ||
                y >= height)
            {
                return;
            }

            var rect = new Int32Rect(x, y, 1, 1);
            byte[] colorData = { color.B, color.G, color.R, 255 }; // B G R

            bitmap.WritePixels(rect, colorData, 4, 0);
        }
    }
}
