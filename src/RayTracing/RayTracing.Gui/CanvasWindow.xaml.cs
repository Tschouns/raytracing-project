using RayTracing.Targets;
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace RayTracing.Gui
{
    /// <summary>
    /// Interaction logic for CanvasWindow.xaml
    /// </summary>
    public partial class CanvasWindow : Window, ICanvas
    {
        private readonly object sync = new object();
        private readonly PixelPipeline pipeline = new PixelPipeline();
        private readonly DispatcherTimer timer = new DispatcherTimer();

        private WriteableBitmap bitmap;

        public CanvasWindow()
        {
            InitializeComponent();

            // Render options
            RenderOptions.SetBitmapScalingMode(this.image, BitmapScalingMode.NearestNeighbor);
            RenderOptions.SetEdgeMode(this.image, EdgeMode.Aliased);

            // Image
            this.image.Stretch = Stretch.Uniform;
            this.image.HorizontalAlignment = HorizontalAlignment.Center;
            this.image.VerticalAlignment = VerticalAlignment.Center;

            // Bitmap
            this.bitmap = InitBitmap(10, 10); // Dummy
            this.Size(640, 480);

            for (int i = 0; i < 256; i++)
            {
                for (int j = 0; j < 256; j++)
                {
                    DrawPixelInternal(new Pixel(i, j, System.Drawing.Color.FromArgb(255, i, j)));
                }
            }

            // Timer
            this.timer.Interval = TimeSpan.FromSeconds(0.1);
            this.timer.Tick += this.Timer_Tick;
            this.timer.Start();
        }

        public IRenderTarget Draw => this.pipeline;
        public void Size(int width, int height)
        {
            lock (this.sync)
            {
                this.pipeline.Clear();
                this.bitmap = InitBitmap(width, height);
                image.Source = this.bitmap;
            }
        }

        public void Fill(System.Drawing.Color color)
        {
            lock (this.sync)
            {
                for (var x = 0; x < this.bitmap.PixelWidth; x++)
                {
                    for (var y = 0; y < this.bitmap.PixelHeight; y++)
                    {
                        this.Draw.Pixel(x, y, color);
                    }
                }

                this.pipeline.ProcessEach(DrawPixelInternal);
            }
        }

        private static WriteableBitmap InitBitmap(int width, int height)
        {
            return new WriteableBitmap(width, height, 96, 96, PixelFormats.Bgr32, null);
        }

        /// <summary>
        /// See: https://learn.microsoft.com/en-us/dotnet/api/system.windows.media.imaging.writeablebitmap?view=windowsdesktop-9.0&redirectedfrom=MSDN#examples
        /// The DrawPixel method updates the WriteableBitmap by using
        /// unsafe code to write a pixel into the back buffer.
        /// </summary>
        private void DrawPixelInternal(Pixel pixel)
        {
            if (pixel.X < 0 || pixel.Y < 0 ||
                pixel.X >= this.bitmap.PixelWidth || pixel.Y >= this.bitmap.PixelHeight)
            {
                throw new IndexOutOfRangeException($"The specified pixel coordinates ({pixel.X};{pixel.Y}) are out of bounds.");
            }

            var column = pixel.X;
            var row = pixel.Y;

            try
            {
                // Reserve the back buffer for updates.
                bitmap.Lock();

                unsafe
                {
                    // Get a pointer to the back buffer.
                    IntPtr pBackBuffer = bitmap.BackBuffer;

                    // Find the address of the pixel to draw.
                    pBackBuffer += row * bitmap.BackBufferStride;
                    pBackBuffer += column * 4;

                    // Compute the pixel's color.
                    int color_data = pixel.Color.R << 16; // R
                    color_data |= pixel.Color.G << 8;   // G
                    color_data |= pixel.Color.B << 0;   // B

                    // Assign the color data to the pixel.
                    *((int*)pBackBuffer) = color_data;
                }

                // Specify the area of the bitmap that changed.
                bitmap.AddDirtyRect(new Int32Rect(column, row, 1, 1));
            }
            finally
            {
                // Release the back buffer and make it available for display.
                bitmap.Unlock();
            }
        }

        /// <summary>
        /// Handles timer events. Processes the pipeline, obtaining the lock.
        /// </summary>
        private void Timer_Tick(object? sender, EventArgs args)
        {
            lock (this.sync)
            {
                this.pipeline.ProcessEach(DrawPixelInternal);
            }
        }
    }
}
