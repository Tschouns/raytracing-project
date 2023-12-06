
using System.Drawing;

namespace RayTracing.Rendering.Settings
{
    public class RenderSettings : IRenderSettings
    {
        public bool FillBackground { get; set; } = true;
        public Color FillBackgroundColor { get; set; } = Color.CornflowerBlue;
        public Color AmbientLightColor { get; set; } = Color.DarkSlateGray;
        public Color DepthCueingColor { get; set; } = Color.AliceBlue;
        public float DepthCueingMaxDistance { get; set; } = 100f;
        public int MaxRecursionDepth { get; set; } = 10;
    }
}
