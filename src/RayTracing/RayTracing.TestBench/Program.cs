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
            ushort resX = 100;
            ushort resY = 80;

            // Create a canvas window.
            var canvasWindow = new CanvasWindow();
            canvasWindow.Size(resX, resY);
            canvasWindow.Fill(System.Drawing.Color.DarkSlateGray);

            var renderTask = Task.Run(() =>
            {
                // Perform example rendering.
                ExamplesRendering.DummyScene(canvasWindow.Draw, resX, resY);
            });

            // Show as dialog (blocking, until the user closes it).
            canvasWindow.ShowDialog();

            //renderTask.Wait();

            Console.WriteLine("Done.");
            Console.ReadLine();
        }
    }
}