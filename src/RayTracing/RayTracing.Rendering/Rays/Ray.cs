using RayTracing.Base;
using RayTracing.Math;
using RayTracing.Math.Calculations;
using RayTracing.Model;

namespace RayTracing.Rendering.Rays
{
    /// <summary>
    /// Represents a single ray to send through a scene.
    /// </summary>
    public class Ray
    {
        public Ray(Vector3 origin, Vector3 direction, Face? originFace = null, bool isInsideObject = false)
        {
            if (isInsideObject && originFace == null)
            {
                throw new ArgumentException("A ray inside an object must have an origin face, i.e. the face where it entered.");
            }

            Origin = origin;
            Direction = direction;
            OriginFace = originFace;
            IsInsideObject = isInsideObject;
        }

        public Vector3 Origin { get; }
        public Vector3 Direction { get; }
        public Face? OriginFace { get; }
        public bool IsInsideObject { get; }

        public bool HasAnyHit(IEnumerable<Face> faces)
        {
            Argument.AssertNotNull(faces, nameof(faces));

            foreach (var face in faces)
            {
                if (this.DetectForwardHitInternal(face, out _))
                {
                    return true;
                }
            }

            return false;
        }

        public RayHit? DetectNearestHit(IEnumerable<Face> faces)
        {
            Argument.AssertNotNull(faces, nameof(faces));

            Vector3? firstHit = null;
            Face? hitFace = null;

            foreach (var face in faces)
            {
                if (this.DetectForwardHitInternal(face, out var intersectionPoint))
                {
                    if (firstHit == null)
                    {
                        firstHit = intersectionPoint;
                        hitFace = face;
                        continue;
                    }

                    if ((intersectionPoint - Origin).LengthSquared() <
                        (firstHit.Value - Origin).LengthSquared())
                    {
                        firstHit = intersectionPoint;
                        hitFace = face;
                    }
                }
            }

            if (!firstHit.HasValue)
            {
                return null;
            }

            var distance = (firstHit.Value - Origin).Length();

            return new RayHit(hitFace!, firstHit.Value, distance);
        }

        private bool DetectForwardHitInternal(Face face, out Vector3 intersectionPoint)
        {
            if (face == this.OriginFace)
            {
                intersectionPoint = new Vector3();
                return false;
            }

            // Back-face culling.
            if (this.CullFace(face))
            {
                intersectionPoint = new Vector3();
                return false;
            }

            // Detect intersection.
            var hitLine = new Line3D(Origin, Origin + Direction);
            var intersect = VectorCalculator3D.IntersectTriangle(hitLine, face.Triangle);
            if (!intersect.HasIntersection)
            {
                intersectionPoint = new Vector3();
                return false;
            }

            // Filter out triangles "behind" the ray.
            if (intersect.Lambda < 0)
            {
                intersectionPoint = new Vector3();
                return false;
            }

            intersectionPoint = intersect.IntersectionPoint!.Value;
            return true;
        }

        private bool CullFace(Face face)
        {
            // If we are inside an object...
            if (IsInsideObject &&
                face.ParentGeometry == OriginFace!.ParentGeometry)
            {
                // ...no back-face culling of faces of that object.
                return false;
            }

            // Regular back-face culling.
            return (face.Normal.Dot(this.Direction) > 0);
        }
    }
}