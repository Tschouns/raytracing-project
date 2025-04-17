
using RayTracing.Model;
using System.Drawing;

namespace RayTracing.Rendering.Settings
{
    public class RenderSettings : IRenderSettings
    {
        public bool FillBackground { get; set; } = true;
        public bool ApplyDepthCueing { get; set; } = true;
        public bool ApplyShadows { get; set; } = true;
        public bool ApplyGloss { get; set; } = true;
        public bool ApplyNormalShading { get; set; } = true;
        public bool ApplyReflections { get; set; } = true;
        public bool ApplyTransmission { get; set; } = true;
        public bool UseFancyLighting { get; set; } = true;
        public ArgbColor FillBackgroundColor { get; set; } = Color.CornflowerBlue.ToArgbColor();
        public ArgbColor AmbientLightColor { get; set; } = Color.DarkSlateGray.ToArgbColor();
        public ArgbColor DepthCueingColor { get; set; } = Color.AliceBlue.ToArgbColor() ;
        public float DepthCueingMaxDistance { get; set; } = 100f;
        public int MaxRecursionDepth { get; set; } = 10;
    }
}
