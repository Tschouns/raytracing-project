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
        private WriteableBitmap bitmap;

        public CanvasWindow()
        {
            InitializeComponent();
        }

        public void Size(int width, int height)
        {
            var newBitmap = new WriteableBitmap(width, height, 96, 96, PixelFormats.Bgra32, null);
            bitmap = newBitmap;
            image.Source = bitmap;
        }

        public void Fill(System.Drawing.Color color)
        {
            throw new System.NotImplementedException();
        }

        public void Pixel(int x, int y, System.Drawing.Color color)
        {
            throw new System.NotImplementedException();
        }
    }
}
