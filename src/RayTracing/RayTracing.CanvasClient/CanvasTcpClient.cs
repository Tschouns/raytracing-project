using RayTracing.Base;
using RayTracing.Targets;
using System.Drawing;
using System.Globalization;
using System.Net.Sockets;

namespace RayTracing.CanvasClient
{
    public class CanvasTcpClient : ICanvas, IRenderTarget
    {
        private readonly TcpClient innerTcpClient;

        public IRenderTarget Draw => this;

        public CanvasTcpClient(TcpClient innerTcpClient)
        {
            Argument.AssertNotNull(innerTcpClient, nameof(innerTcpClient));

            this.innerTcpClient = innerTcpClient;
        }

        public void Size(int width, int height)
        {
            this.SendMessage($"size {width} {height}");
        }

        public void Pixel(int x, int y, Color color)
        {
            this.SendMessage($"pixel {x} {y} {ColorString(color)}");
        }

        public void Fill(Color color)
        {
            this.SendMessage($"fill {ColorString(color)}");
        }

        public void Quit()
        {
            this.SendMessage($"quit");
        }

        private static string ColorValueString(byte value)
        {
            var number = value / 256f;

            return number.ToString(CultureInfo.InvariantCulture);
        }

        private static string ColorString(Color color)
        {
            var r = ColorValueString(color.R);
            var g = ColorValueString(color.G);
            var b = ColorValueString(color.B);

            var colorString = $"{r} {g} {b}";

            return colorString;
        }

        private void SendMessage(string message)
        {
            // Add new line: terminates the message.
            message += "\r\n";

            // ASCII-encode message.
            var data = System.Text.Encoding.ASCII.GetBytes(message);

            // Get a client stream for reading and writing.
            var stream = this.innerTcpClient.GetStream();

            // Send the message to the connected TcpServer.
            stream.Write(data, 0, data.Length);
        }
    }
}
