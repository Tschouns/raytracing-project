// See https://aka.ms/new-console-template for more information
using RayTracing.Gui;

namespace RayTracing.TestBench
{
    internal class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            // Connect to canvas server.
            //using var tcpClient = new TcpClient();
            //tcpClient.Connect("127.0.0.1", 9012);
            //var canvasClient = new CanvasTcpClient(tcpClient);

            var canvasWindow = new CanvasWindow();
            canvasWindow.Show();

            // Perform example rendering.
            ExamplesRendering.GlassScene(canvasWindow);

            Console.WriteLine("Done.");
            Console.ReadLine();

            canvasWindow.Close();
        }
    }
}

//tcpClient.Close();