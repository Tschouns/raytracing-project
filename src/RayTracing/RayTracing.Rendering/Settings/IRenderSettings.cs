
using System.Drawing;

namespace RayTracing.Rendering.Settings
{
    /// <summary>
    /// Specifies a number of settings for the rendering process.
    /// </summary>
    public interface IRenderSettings
    {
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
