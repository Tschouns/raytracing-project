// See https://aka.ms/new-console-template for more information
using RayTracing.CanvasClient;
using RayTracing.TestBench;
using System.Net.Sockets;

Console.WriteLine("Hello, World!");

using var tcpClient = new TcpClient();
tcpClient.Connect("127.0.0.1", 9012);
//tcpClient.Connect("192.168.0.100", 9012);

var canvasClient = new CanvasTcpClient(tcpClient);

//Examples.DrawLine(canvasClient);
//Examples2D.DrawIntersectingLines(canvasClient);
Examples3D.DrawTriangles(canvasClient);

tcpClient.Close();