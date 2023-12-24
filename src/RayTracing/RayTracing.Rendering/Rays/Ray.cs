using RayTracing.Base;
using RayTracing.Math;
using RayTracing.Math.Calculations;
using RayTracing.Model;
using RayTracing.Model.Octree;

namespace RayTracing.Rendering.Rays
{
    /// <summary>
    /// Represents a single ray to send through a scene.
    /// </summary>
    public class Ray
    {
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
                if (DetectForwardHitInternal(face, out _))
                {
                    return true;
                }
            }

            return false;
        }

        public RayHit? DetectNearestHit(IEnumerable<Geometry> geometries)
        {
            Argument.AssertNotNull(geometries, nameof(geometries));

            Vector3? nearestHitPoint = null;
            Face? nearestHitFace = null;
            float? nearestHitDistanceSquared = null;

            foreach (var geometry in geometries)
            {
                var boundingBox = geometry.Octree.BoundingBox;
                var rayAndAabb = VectorCalculator3D.DoesRayIntersectWithAabb(Origin, Direction, boundingBox.Min, boundingBox.Max);

                if (!rayAndAabb.DoIntersect)
                {
                    // Next geometry.
                    continue;
                }

                if (geometry.Octree.HasChildren)
                {
                    if (CheckOctreeNodes(geometry.Octree.ChildNodes, faces => CheckFaces(faces, ref nearestHitPoint, ref nearestHitFace, ref nearestHitDistanceSquared)))
                    {
                        // Next geometry.
                        continue;
                    }
                }
                else
                {
                    if (CheckFaces(geometry.Octree.Faces, ref nearestHitPoint, ref nearestHitFace, ref nearestHitDistanceSquared))
                    {
                        // Next geometry.
                        continue;
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
                Direction,
                distance,
                nearestHitFace!,
                isBackFaceHit: InsideObjects.Contains(nearestHitFace.ParentGeometry)); // That means we're hitting the face from inside the object.
        }

        private bool CheckOctreeNodes(OctreeNode[]? nodes, Func<IEnumerable<Face>, bool> checkFaces)
        {
            if (nodes.Length != 8)
            {
                throw new ArgumentException("Each octree level is supposed to have 8 nodes.");
            }

            var relevantNodesOrdered = nodes
                .Select(n => new { Node = n, Result = VectorCalculator3D.DoesRayIntersectWithAabb(Origin, Direction, n.BoundingBox.Min, n.BoundingBox.Max) })
                //.Where(n => n.Result.DoIntersect && n.Result.T1 > 0)
                .Where(n => n.Result.DoIntersect)
                .OrderBy(c => c.Result.T0)
                .ToArray(); // Useful for debugging.

            foreach (var node in relevantNodesOrdered.Select(n => n.Node))
            {
                if (node.HasChildren)
                {
                    if (CheckOctreeNodes(node.ChildNodes, checkFaces))
                    {
                        return true;
                    }
                }
                else
                {
                    if (checkFaces(node.Faces))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool CheckFaces(IEnumerable<Face> faces, ref Vector3? nearestHitPoint, ref Face? nearestHitFace, ref float? nearestHitDistanceSquared)
        {
            var anyHit = false;

            foreach (var face in faces)
            {
                if (UpdateNearestHit(face, ref nearestHitPoint, ref nearestHitFace, ref nearestHitDistanceSquared))
                {
                    anyHit = true;
                }
            }

            return anyHit;
        }

        private bool UpdateNearestHit(Face face, ref Vector3? nearestHitPoint, ref Face? nearestHitFace, ref float? nearestHitDistanceSquared)
        {
            if (DetectForwardHitInternal(face, out var currentHitPoint))
            {
                if (nearestHitPoint == null)
                {
                    nearestHitPoint = currentHitPoint;
                    nearestHitFace = face;
                    nearestHitDistanceSquared = (nearestHitPoint.Value - Origin).LengthSquared();

                    return true;
                }

                var currentHitDistanceSquared = (currentHitPoint - Origin).LengthSquared();
                var changeSquared = currentHitDistanceSquared - nearestHitDistanceSquared;

                if (changeSquared < 0)
                {
                    nearestHitPoint = currentHitPoint;
                    nearestHitFace = face;
                    nearestHitDistanceSquared = currentHitDistanceSquared;
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        public RayHit? BACKUP(IEnumerable<Geometry> geometries)
        {
            Argument.AssertNotNull(geometries, nameof(geometries));

            Vector3? nearestHitPoint = null;
            Face? nearestHitFace = null;
            float? nearestHitDistanceSquared = null;

            foreach (var geometry in geometries)
            {
                IEnumerable<Face> faces = geometry.Faces;

                // AABB optimization:
                //faces = geometry.Octree.GetFaces(Origin, Direction, 10);

                foreach (var face in faces)
                {
                    if (DetectForwardHitInternal(face, out var currentHitPoint))
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
            }

            if (!nearestHitPoint.HasValue)
            {
                return null;
            }

            var distance = MathF.Sqrt(nearestHitDistanceSquared!.Value);

            return new RayHit(
                nearestHitPoint.Value,
                Direction,
                distance,
                nearestHitFace!,
                isBackFaceHit: InsideObjects.Contains(nearestHitFace.ParentGeometry)); // That means we're hitting the face from inside the object.
        }

        private bool DetectForwardHitInternal(Face face, out Vector3 intersectionPoint)
        {
            if (face == OriginFace)
            {
                intersectionPoint = new Vector3();
                return false;
            }

            // Back-face culling.
            if (CullFace(face))
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
            if (InsideObjects.Contains(face.ParentGeometry))
            {
                // ...no back-face culling for this object.
                return false;
            }

            // Regular back-face culling.
            return (face.Normal.Dot(Direction) > 0);
        }
    }
}