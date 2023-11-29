using RayTracing.Base;

namespace RayTracing.Model
{
    /// <summary>
    /// Represents a 3D scene.
    /// </summary>
    public class Scene
    {
        public Scene(
            IReadOnlyList<Material> materials,
            IReadOnlyList<Geometry> geometry,
            IReadOnlyList<LightSource> lightSources)
        {
            Argument.AssertNotNull(materials, nameof(materials));
            Argument.AssertNotNull(geometry, nameof(geometry));
            Argument.AssertNotNull(lightSources, nameof(lightSources));

            Materials = materials;
            Geometries = geometry;
            LightSources = lightSources;
        }

        /// <summary>
        /// Gets the materials in this scene.
        /// </summary>
        public IReadOnlyList<Material> Materials { get; }

        /// <summary>
        /// Gets the geometries in the scene.
        /// </summary>
        public IReadOnlyList<Geometry> Geometries { get; }

        /// <summary>
        /// Gets the light sources in the scene.
        /// </summary>
        public IReadOnlyList<LightSource> LightSources { get; }
    }
}
