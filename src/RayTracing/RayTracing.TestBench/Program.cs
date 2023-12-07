// See https://aka.ms/new-console-template for more information
using RayTracing.CanvasClient;
using RayTracing.TestBench;
using System.Net.Sockets;

Console.WriteLine("Hello, World!");

// Connect to canvas server.
using var tcpClient = new TcpClient();
tcpClient.Connect("127.0.0.1", 9012);
var canvasClient = new CanvasTcpClient(tcpClient);

// Perform example rendering.
ExamplesRendering.DummyScene(canvasClient);

tcpClient.Close();