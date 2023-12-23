using RayTracing.Base;
using RayTracing.Math;

namespace RayTracing.Model.Octree
{
    internal static class OctreeHelper
    {
        private static readonly float calcErrorMargin = 0.00001f;

        public static OctreeNode? BuildOctree(IReadOnlyList<Face> allFaces)
        {
            Argument.AssertNotNull(allFaces, nameof(allFaces));

            if (!allFaces.Any())
            {
                return null;
            }

            // Determine the bounding box.
            var aabbTracker = new AabbTracker(allFaces.First().Triangle.CornerA);

            foreach (var face in allFaces)
            {
                aabbTracker.Register(face.Triangle.CornerA);
                aabbTracker.Register(face.Triangle.CornerB);
                aabbTracker.Register(face.Triangle.CornerC);
            }

            var boundingBox = aabbTracker.GetBoundingBox();

            // Find the center.
            var center = boundingBox.Min + (boundingBox.Max - boundingBox.Min).Scale(0.5f);

            // Assign all faces to the different boxes.
            var leftTopFront = new List<Face>();
            var leftTopBack = new List<Face>();
            var leftBottomFront = new List<Face>();
            var leftBottomBack = new List<Face>();
            var rightTopFront = new List<Face>();
            var rightTopBack = new List<Face>();
            var rightBottomFront = new List<Face>();
            var rightBottomBack = new List<Face>();

            foreach (var face in allFaces)
            {
                AssignToList(
                    face,
                    center,
                    leftTopFront,
                    leftTopBack,
                    leftBottomFront,
                    leftBottomBack,
                    rightTopFront,
                    rightTopBack,
                    rightBottomFront,
                    rightBottomBack);
            }

            // Create child nodes.
            var child1 = BuildChildIfSmaller(allFaces.Count, leftTopFront);
            var child2 = BuildChildIfSmaller(allFaces.Count, leftTopBack);
            var child3 = BuildChildIfSmaller(allFaces.Count, leftBottomFront);
            var child4 = BuildChildIfSmaller(allFaces.Count, leftBottomBack);
            var child5 = BuildChildIfSmaller(allFaces.Count, rightTopFront);
            var child6 = BuildChildIfSmaller(allFaces.Count, rightTopBack);
            var child7 = BuildChildIfSmaller(allFaces.Count, rightBottomFront);
            var child8 = BuildChildIfSmaller(allFaces.Count, rightBottomBack);

            return new OctreeNode(boundingBox, allFaces, child1, child2, child3, child4, child5, child6, child7, child8);
        }

        private static void AssignToList(
            Face face,
            Vector3 center,
            IList<Face> leftTopFront,
            IList<Face> leftTopBack,
            IList<Face> leftBottomFront,
            IList<Face> leftBottomBack,
            IList<Face> rightTopFront,
            IList<Face> rightTopBack,
            IList<Face> rightBottomFront,
            IList<Face> rightBottomBack)
        {
            foreach (var vertex in GetVertices(face))
            {
                var left = vertex.X > center.X;
                var top = vertex.Y > center.Y;
                var front = vertex.Z > center.Z;

                if (left)
                {
                    if (top)
                    {
                        if (front)
                        {
                            leftTopFront.Add(face);
                            return;
                        }
                        else
                        {
                            leftTopBack.Add(face);
                            return;
                        }
                    }
                    else
                    {
                        if (front)
                        {
                            leftBottomFront.Add(face);
                            return;
                        }
                        else
                        {
                            leftBottomBack.Add(face);
                            return;
                        }
                    }
                }
                else
                {
                    if (top)
                    {
                        if (front)
                        {
                            rightTopFront.Add(face);
                            return;
                        }
                        else
                        {
                            rightTopBack.Add(face);
                            return;
                        }
                    }
                    else
                    {
                        if (front)
                        {
                            rightBottomFront.Add(face);
                            return;
                        }
                        else
                        {
                            rightBottomBack.Add(face);
                            return;
                        }
                    }
                }
            }
        }

        private static OctreeNode? BuildChildIfSmaller(int previousFaceCount, IReadOnlyList<Face> faces)
        {
            if (faces.Count < previousFaceCount)
            {
                return BuildOctree(faces);
            }
            else
            {
                return null;
            }
        }

        //private static void AddIfNotJustAdded(Face face, IList<Face> facesList)
        //{
        //    if (!facesList.Any())
        //    {
        //        facesList.Add(face);
        //        return;
        //    }

        //    if (facesList.Last() == face)
        //    {
        //        return;
        //    }

        //    facesList.Add(face);
        //}

        private static IEnumerable<Vector3> GetVertices(Face face)
        {
            Argument.AssertNotNull(face, nameof(face));

            return new Vector3[] { face.Triangle.CornerA, face.Triangle.CornerB, face.Triangle.CornerC };
        }

        private class AabbTracker
        {
            private float minX;
            private float minY;
            private float minZ;

            private float maxX;
            private float maxY;
            private float maxZ;

            public AabbTracker(Vector3 vertex)
            {
                minX = vertex.X;
                minY = vertex.Y;
                minZ = vertex.Z;

                maxX = vertex.X;
                maxY = vertex.Y;
                maxZ = vertex.Z;
            }

            public AxisAlignedBoundingBox GetBoundingBox()
            {
                return new AxisAlignedBoundingBox(
                    new Vector3(
                        minX - calcErrorMargin,
                        minY - calcErrorMargin,
                        minZ - calcErrorMargin),
                    new Vector3(
                        maxX + calcErrorMargin,
                        maxY + calcErrorMargin,
                        maxZ + calcErrorMargin));
            }

            public void Register(Vector3 vertex)
            {
                // Min.
                if (vertex.X < minX)
                {
                    minX = vertex.X;
                }

                if (vertex.Y < minY)
                {
                    minY = vertex.Y;
                }

                if (vertex.Z < minZ)
                {
                    minZ = vertex.Z;
                }

                // Max.
                if (vertex.X > maxX)
                {
                    maxX = vertex.X;
                }

                if (vertex.Y > maxY)
                {
                    maxY = vertex.Y;
                }

                if (vertex.Z > maxZ)
                {
                    maxZ = vertex.Z;
                }
            }
        }
    }
}
