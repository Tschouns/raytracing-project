using RayTracing.Targets;
using System;
using System.Collections.Concurrent;
using System.Drawing;

namespace RayTracing.Gui
{
    public class PixelPipeline : IRenderTarget
    {
        private readonly ConcurrentQueue<Pixel> queue = new();

        public void Pixel(int x, int y, Color color)
        {
            this.queue.Enqueue(new Pixel(x, y, color));
        }

        public void Clear()
        {
            this.queue.Clear();
        }

        public void ProcessEach(Action<Pixel> process)
        {
            while (true)
            {
                if (this.queue.TryDequeue(out var pixel))
                {
                    process(pixel);
                }
                else
                {
                    return;
                }
            }
        }
    }
}
