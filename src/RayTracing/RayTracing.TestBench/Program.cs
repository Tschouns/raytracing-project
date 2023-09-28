// See https://aka.ms/new-console-template for more information
using RayTracing.CanvasClient;
using System.Drawing;
using System.Net.Sockets;

Console.WriteLine("Hello, World!");

using var tcpClient = new TcpClient();
tcpClient.Connect("127.0.0.1", 9012);
//tcpClient.Connect("192.168.0.100", 9012);

var canvasClient = new CanvasTcpClient(tcpClient);

var x = 30;
var y = 20;

canvasClient.Size(x, y);
canvasClient.Fill(Color.HotPink);

var i = 0;
while (i < x && i < y)
{
    canvasClient.Pixel(i, i, Color.GreenYellow);
    i++;
}

tcpClient.Close();