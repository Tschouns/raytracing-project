using RayTracing.Base;
using RayTracing.Math;
using RayTracing.Math.Calculations;
using RayTracing.Model;
using RayTracing.Model.Octrees;

namespace RayTracing.Rendering.Rays
{
    /// <summary>
    /// Represents a single ray to send through a scene.
    /// </summary>
    public class Ray
    {
        private static int[] COUNTER = new int[8];

        public Ray(Vector3 origin, Vector3 direction, Face? originFace = null, IEnumerable<Geometry> insideObjects = null)
        {
            Origin = origin;
            Direction = direction.Norm()!.Value;
            OriginFace = originFace;
            InsideObjects = (insideObjects ?? new Geometry[0]).ToArray(); // Make a copy of the list.
        }

        public Vector3 Origin { get; }
        public Vector3 Direction { get; }
        public Face? OriginFace { get; }
        public IEnumerable<Geometry> InsideObjects { get; }

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

        public RayHit? DetectNearestHit(Octree octree)
        {
            return this.DetectNearestHit(new Octree[] { octree });
        }

        private RayHit? DetectNearestHit(IEnumerable<Octree> octrees)
        {
            var octreeTests = octrees
                .Where(o => o.AllFaces.Any())
                .Select(o => new { Octree = o, Hit = AabbHelper.IntersectAabb(this.Origin, this.Direction, o.BoundingBox.Min, o.BoundingBox.Max) })
                .ToList();
               
            var octreeHitsOrdered = octreeTests
                .Where(o => o.Hit.DoIntersect)
                .OrderBy(o => o.Hit.T0)
                .ToList();

            var count = octreeHitsOrdered.Count();
            //COUNTER[count - 1]++;
            //var hits = new List<RayHit>();

            foreach (var octreeHit in octreeHitsOrdered)
            {
                if (octreeHit.Octree.HasChildren)
                {
                    // Recursive call:
                    var rayHit = this.DetectNearestHit(octreeHit.Octree.Children);
                    if (rayHit != null)
                    {
                        //hits.Add(rayHit);
                        return rayHit;
                    }
                }
                else
                {
                    var rayHit = this.DetectNearestHit(octreeHit.Octree.AllFaces);
                    if (rayHit != null)
                    {
                        //hits.Add(rayHit);
                        return rayHit;
                    }
                }
            }

            //return hits.OrderBy(hit => hit.Distance).FirstOrDefault();
            return null;
        }

        private RayHit? DetectNearestHit(IEnumerable<Face> faces)
        {
            Argument.AssertNotNull(faces, nameof(faces));

            Vector3? nearestHitPoint = null;
            Face? nearestHitFace = null;
            float? nearestHitDistanceSquared = null;

            foreach (var face in faces)
            {
                if (this.DetectForwardHitInternal(face, out var currentHitPoint))
                {
                    if (nearestHitPoint == null)
                    {
                        nearestHitPoint = currentHitPoint;
                        nearestHitFace = face;
                        nearestHitDistanceSquared = (nearestHitPoint.Value - Origin).LengthSquared();

                        continue;
                    }

                    var currentHitDistanceSquared = (currentHitPoint - Origin).LengthSquared();
                    var changeSquared = currentHitDistanceSquared - nearestHitDistanceSquared;

                    if (changeSquared < 0)
                    {
                        nearestHitPoint = currentHitPoint;
                        nearestHitFace = face;
                        nearestHitDistanceSquared = currentHitDistanceSquared;
                    }
                }
            }

            if (!nearestHitPoint.HasValue)
            {
                return null;
            }

            var distance = MathF.Sqrt(nearestHitDistanceSquared!.Value);

            return new RayHit(
                nearestHitPoint.Value,
                this.Direction,
                distance, 
                nearestHitFace!,
                isBackFaceHit: InsideObjects.Contains(nearestHitFace.ParentGeometry)); // That means we're hitting the face from inside the object.
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
            // If we are inside the object...
            if (this.InsideObjects.Contains(face.ParentGeometry))
            {
                // ...no back-face culling for this object.
                return false;
            }

            // Regular back-face culling.
            return (face.Normal.Dot(this.Direction) > 0);
        }
    }
}