
using RayTracing.Base;
using RayTracing.Math;
using RayTracing.Math.Calculations;
using RayTracing.Model;

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

        public RayHit? DetectNearestHit(IEnumerable<Face> faces)
        {
            Argument.AssertNotNull(faces, nameof(faces));

            var hitLine = new Line3D(Origin, Origin + Direction);
            Vector3? firstHit = null;

            // TODO: filter out trianlges behind the camera.

            foreach (var face in faces)
            {
                var intersect = VectorCalculator3D.IntersectTriangle(hitLine, face.Triangle);
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

            var distance = (firstHit.Value - Origin).Length();

            return new RayHit(firstHit.Value, distance);
        }
    }
}