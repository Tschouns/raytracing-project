using RayTracing.Base;

namespace RayTracing.Model
{
    /// <summary>
    /// Represents a 3D scene.
    /// </summary>
    public class Scene
    {
        public Scene(IReadOnlyList<Geometry> geometry)
        {
            Argument.AssertNotNull(geometry, nameof(geometry));

            Geometries = geometry;
        }

        public IReadOnlyList<Geometry> Geometries { get; }
    }
}
