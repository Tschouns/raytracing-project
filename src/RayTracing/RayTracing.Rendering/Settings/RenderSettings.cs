
using System.Drawing;

namespace RayTracing.Rendering.Settings
{
    public class RenderSettings : IRenderSettings
    {
        public bool FillBackground { get; set; } = true;
        public bool ApplyDepthCueing { get; set; } = true;
        public bool ApplyShadows { get; set; } = true;
        public bool ApplyNormalShading { get; set; } = true;
        public bool ApplyReflections { get; set; } = true;
        public bool ApplyTransmission { get; set; } = true;
        public Color FillBackgroundColor { get; set; } = Color.CornflowerBlue;
        public Color AmbientLightColor { get; set; } = Color.DarkSlateGray;
        public Color DepthCueingColor { get; set; } = Color.AliceBlue;
        public float DepthCueingMaxDistance { get; set; } = 100f;
        public int MaxRecursionDepth { get; set; } = 10;
    }
}
