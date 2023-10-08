
using RayTracing.Base;
using RayTracing.Math;
using RayTracing.Math.Calculations;

namespace RayTracing.Rendering
{
    /// <summary>
    /// Represents a single ray to send through a scene.
    /// </summary>
    public class Ray
    {
        public Ray(Vector3 origin, Vector3 direction)
        {
            Origin = origin;
            Direction = direction;
        }

        public Vector3 Origin { get; set; }
        public Vector3 Direction { get; set; }

        public RayHit? DetectNearestHit(IEnumerable<Triangle3D> triangles)
        {
            Argument.AssertNotNull(triangles, nameof(triangles));

            var hitLine = new Line3D(Origin, Origin + Direction);
            Vector3? firstHit = null;

            // TODO: filter out trianlges behind the camera.

            foreach (var triangle in triangles)
            {
                var intersect = VectorCalculator3D.IntersectTriangle(hitLine, triangle);
                if (intersect == null)
                {
                    continue;
                }

                if (firstHit == null)
                {
                    firstHit = intersect;
                    continue;
                }

                if ((intersect.Value-Origin).LengthSquared() < 
                    (firstHit.Value-Origin).LengthSquared())
                {
                    firstHit = intersect;
                }
            }

            if (!firstHit.HasValue)
            {
                return null;
            }

            return new RayHit(firstHit.Value);
        }
    }
}