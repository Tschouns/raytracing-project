using RayTracing.Base;
using RayTracing.Model.Octrees;

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
            Octree octree,
            IReadOnlyList<LightSource> lightSources)
        {
            Argument.AssertNotNull(materials, nameof(materials));
            Argument.AssertNotNull(geometry, nameof(geometry));
            Argument.AssertNotNull(octree, nameof(octree));
            Argument.AssertNotNull(lightSources, nameof(lightSources));

            this.Materials = materials;
            this.Geometries = geometry;
            this.Octree = octree;
            this.LightSources = lightSources;
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
        /// Gets the octree which contains all the faces within the scene.
        /// </summary>
        public Octree Octree { get; }

        /// <summary>
        /// Gets the light sources in the scene.
        /// </summary>
        public IReadOnlyList<LightSource> LightSources { get; }
    }
}
