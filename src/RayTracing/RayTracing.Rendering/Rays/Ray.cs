﻿using RayTracing.Base;
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
        public Ray(Vector3 origin, Vector3 direction)
        {
            Origin = origin;
            Direction = direction;
        }

        public Vector3 Origin { get; set; }
        public Vector3 Direction { get; set; }

        public bool HasAnyHit(IEnumerable<Face> faces)
        {
            Argument.AssertNotNull(faces, nameof(faces));

            var hitLine = new Line3D(Origin, Origin + Direction);

            foreach (var face in faces)
            {
                var intersect = VectorCalculator3D.IntersectTriangle(hitLine, face.Triangle);
                if (intersect.HasIntersection &&
                    intersect.Lambda > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public RayHit? DetectNearestHit(IEnumerable<Face> faces)
        {
            Argument.AssertNotNull(faces, nameof(faces));

            var hitLine = new Line3D(Origin, Origin + Direction);
            Vector3? firstHit = null;
            Face? hitFace = null;

            foreach (var face in faces)
            {
                var intersect = VectorCalculator3D.IntersectTriangle(hitLine, face.Triangle);
                if (!intersect.HasIntersection)
                {
                    continue;
                }

                // Filter out trianlges behind the camera.
                if (intersect.Lambda < 0)
                {
                    continue;
                }

                if (firstHit == null)
                {
                    firstHit = intersect.IntersectionPoint;
                    hitFace = face;
                    continue;
                }

                if ((intersect.IntersectionPoint!.Value - Origin).LengthSquared() <
                    (firstHit.Value - Origin).LengthSquared())
                {
                    firstHit = intersect.IntersectionPoint;
                    hitFace = face;
                }
            }

            if (!firstHit.HasValue)
            {
                return null;
            }

            var distance = (firstHit.Value - Origin).Length();

            return new RayHit(hitFace!, firstHit.Value, distance);
        }
    }
}