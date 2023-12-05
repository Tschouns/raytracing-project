using RayTracing.Base;

namespace RayTracing.Model
{
    /// <summary>
    /// Represents a 3D scene.
    /// </summary>
    public class Scene
    {
        public Scene(
            IReadOnlyList<Geometry> geometry,
            IReadOnlyList<LightSource> lightSources)
        {
            Argument.AssertNotNull(geometry, nameof(geometry));
            Argument.AssertNotNull(lightSources, nameof(lightSources));

            Geometries = geometry;
            LightSources = lightSources;
        }

        public IReadOnlyList<Geometry> Geometries { get; }
        public IReadOnlyList<LightSource> LightSources { get; }
    }
}
