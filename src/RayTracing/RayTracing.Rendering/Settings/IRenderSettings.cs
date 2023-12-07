
using System.Drawing;

namespace RayTracing.Rendering.Settings
{
    /// <summary>
    /// Specifies a number of settings for the rendering process.
    /// </summary>
    public interface IRenderSettings
    {
        /// <summary>
        /// Gets a value indicating whether the screen shall be filled with a background
        /// color before rendering actual pixels.
        /// </summary>
        public bool FillBackground { get;}

        /// <summary>
        /// Gets a value indicating whether the rendering shall apply depth cueing.
        /// </summary>
        public bool ApplyDepthCueing { get; }

        /// <summary>
        /// Gets a value indicating whether the rendering shall apply normal shading.
        /// </summary>
        public bool ApplyNormalShading { get; }

        /// <summary>
        /// Gets a value indicating whether the rendering shall apply shodows.
        /// </summary>
        public bool ApplyShadows { get; }

        /// <summary>
        /// Gets a value indicating whether the rendering shall apply reflections.
        /// </summary>
        public bool ApplyReflections { get; }

        /// <summary>
        /// Gets a value indicating whether the rendering shall apply transmission.
        /// </summary>
        public bool ApplyTransmission { get; }

        /// <summary>
        /// Gets the background color to fill the screen with.
        /// </summary>
        public Color FillBackgroundColor { get; }

        /// <summary>
        /// Gets the ambient light color, i.e. the light before any light sources.
        /// </summary>
        Color AmbientLightColor { get; }

        /// <summary>
        /// Gets the depth cueing color, i.e. the color objects at a distance fade to.
        /// </summary>
        Color DepthCueingColor { get; }

        /// <summary>
        /// Gets the maximum depth cueing distance, i.e. the distance at which objects fully blend with the background color.
        /// </summary>
        float DepthCueingMaxDistance { get; }

        /// <summary>
        /// Gets the maximum depth of the recursive ray traycing.
        /// </summary>
        int MaxRecursionDepth { get; }
    }
}
