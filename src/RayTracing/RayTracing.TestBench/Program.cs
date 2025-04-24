using RayTracing.Gui;

namespace RayTracing.TestBench
{
    internal class Program
    {
        [STAThread]
        private static void Main()
        {
            Console.WriteLine("Hello, World!");

            // Resolution
            ushort resX = 300;
            ushort resY = 400;

            // Create a canvas window.
            var canvasWindow = new CanvasWindow();
            canvasWindow.Size(resX, resY);
            canvasWindow.Fill(System.Drawing.Color.DarkRed);

            var renderTask = Task.Run(() =>
            {
                // Perform example rendering.
                ExamplesRendering.GlassScene(canvasWindow.Draw, resX, resY);
            });

            // Show as dialog (blocking, until the user closes it).
            canvasWindow.ShowDialog();

            //renderTask.Wait();

            Console.WriteLine("Done.");
            Console.ReadLine();
        }
    }
}