
using RayTracing.Math.Calculations;

namespace RayTracing.Math.Tests.Calculations
{
    public class VectorCalculator3DTests
    {
        [Fact]
        public void IntersectPlane_ZLineThroughXyPlane_ReturnsExpectedIntersection()
        {
            IntersectPlane_GivenLineAndPlane_ReturnsExpectedResult(
                new Line3D(
                    pointA: new Vector3(1, 1, -10),
                    pointB: new Vector3(1, 1, -9)),
                new Plane3D(
                    pointP: new Vector3(0, 0, 0),
                    vectorU: new Vector3(3, 0, 0),
                    vectorV: new Vector3(0, 5, 0)),
                new Vector3(1, 1, 0));
        }

        [Fact]
        public void IntersectPlane_AngledLineAndPlane_ReturnsExpectedIntersection()
        {
            IntersectPlane_GivenLineAndPlane_ReturnsExpectedResult(
                new Line3D(
                    pointA: new Vector3(-5, 5, 30),
                    pointB: new Vector3(-5, 5, 29)),
                new Plane3D(
                    pointP: new Vector3(-2, 3, 7),
                    vectorU: new Vector3(1, 2, -1),
                    vectorV: new Vector3(-1, 2, -1)),
                new Vector3(-5, 5, 6));
        }

        [Fact]
        public void IntersectPlane_LineParallelToPlane_ReturnsNull()
        {
            IntersectPlane_GivenLineAndPlane_ReturnsExpectedResult(
                new Line3D(
                    pointA: new Vector3(7, 0, 1),
                    pointB: new Vector3(0, 3, 1)),
                new Plane3D(
                    pointP: new Vector3(0, 0, 0),
                    vectorU: new Vector3(3, 0, 0),
                    vectorV: new Vector3(0, 5, 0)),
                null);
        }

        [Fact]
        public void IntersectTriangle_ZLineThroughXyTriangle_ReturnsExpectedIntersection()
        {
            IntersectTriangle_GivenLineAndTriangle_ReturnsExpectedResult(
                new Line3D(
                    pointA: new Vector3(1, 1, -10),
                    pointB: new Vector3(1, 1, -9)),
                new Triangle3D(
                    cornerA: new Vector3(0, 0, 0),
                    cornerB: new Vector3(2, 0, 0),
                    cornerC: new Vector3(0, 2, 0)),
                new Vector3(1, 1, 0));
        }

        [Fact]
        public void IntersectTriangle_AngledLineAndTriangle_ReturnsExpectedIntersection()
        {
            IntersectTriangle_GivenLineAndTriangle_ReturnsExpectedResult(
                new Line3D(
                    pointA: new Vector3(-7, -1, -6),
                    pointB: new Vector3(-6, 3, -10)),
                new Triangle3D(
                    cornerA: new Vector3(-8, -1, -10),
                    cornerB: new Vector3(-7, 3, -6),
                    cornerC: new Vector3(17, 3, -6)),
                new Vector3(-6.5f, 1, -8));
        }

        [Fact]
        public void IntersectTriangle_ZLineThroughCornerA_ReturnsExpectedIntersection()
        {
            IntersectTriangle_GivenLineAndTriangle_ReturnsExpectedResult(
                new Line3D(
                    pointA: new Vector3(2, 2, 3),
                    pointB: new Vector3(2, 2, 4)),
                new Triangle3D(
                    cornerA: new Vector3(2, 2, -3),
                    cornerB: new Vector3(4, 2, -3),
                    cornerC: new Vector3(2, 4, -3)),
                new Vector3(2, 2, -3));
        }

        [Fact]
        public void IntersectTriangle_ZLineThroughCornerB_ReturnsExpectedIntersection()
        {
            IntersectTriangle_GivenLineAndTriangle_ReturnsExpectedResult(
                new Line3D(
                    pointA: new Vector3(4, 2, 3),
                    pointB: new Vector3(4, 2, 4)),
                new Triangle3D(
                    cornerA: new Vector3(2, 2, -3),
                    cornerB: new Vector3(4, 2, -3),
                    cornerC: new Vector3(2, 4, -3)),
                new Vector3(4, 2, -3));
        }

        [Fact]
        public void IntersectTriangle_ZLineThroughCornerC_ReturnsExpectedIntersection()
        {
            IntersectTriangle_GivenLineAndTriangle_ReturnsExpectedResult(
                new Line3D(
                    pointA: new Vector3(2, 4, -5),
                    pointB: new Vector3(2, 4, -6)),
                new Triangle3D(
                    cornerA: new Vector3(2, 2, -3),
                    cornerB: new Vector3(4, 2, -3),
                    cornerC: new Vector3(2, 4, -3)),
                new Vector3(2, 4, -3));
        }

        [Fact]
        public void IntersectTriangle_JustOffCornerA_ReturnsNull()
        {
            IntersectTriangle_GivenLineAndTriangle_ReturnsExpectedResult(
                new Line3D(
                    pointA: new Vector3(1.99f, 1.99f, 3),
                    pointB: new Vector3(1.99f, 1.99f, 4)),
                new Triangle3D(
                    cornerA: new Vector3(2, 2, -3),
                    cornerB: new Vector3(4, 2, -3),
                    cornerC: new Vector3(2, 4, -3)),
                null);
        }

        [Fact]
        public void IntersectTriangle_JustOffCornerB_ReturnsNull()
        {
            IntersectTriangle_GivenLineAndTriangle_ReturnsExpectedResult(
                new Line3D(
                    pointA: new Vector3(4.01f, 2.01f, 3),
                    pointB: new Vector3(4.01f, 2.01f, 4)),
                new Triangle3D(
                    cornerA: new Vector3(2, 2, -3),
                    cornerB: new Vector3(4, 2, -3),
                    cornerC: new Vector3(2, 4, -3)),
                null);
        }

        [Fact]
        public void IntersectTriangle_JustOffCornerC_ReturnsNull()
        {
            IntersectTriangle_GivenLineAndTriangle_ReturnsExpectedResult(
                new Line3D(
                    pointA: new Vector3(2.01f, 4.01f, 3),
                    pointB: new Vector3(2.01f, 4.01f, 4)),
                new Triangle3D(
                    cornerA: new Vector3(2, 2, -3),
                    cornerB: new Vector3(4, 2, -3),
                    cornerC: new Vector3(2, 4, -3)),
                null);
        }

        private void IntersectPlane_GivenLineAndPlane_ReturnsExpectedResult(
            Line3D line,
            Plane3D plane,
            Vector3? expectedIntersection)
        {
            // Act
            var actualIntersection = VectorCalculator3D.IntersectPlane(line, plane);

            // Assert
            Assert.Equal(expectedIntersection, actualIntersection);
        }

        private void IntersectTriangle_GivenLineAndTriangle_ReturnsExpectedResult(
            Line3D line,
            Triangle3D triangle,
            Vector3? expectedIntersection)
        {
            // Act
            var result = VectorCalculator3D.IntersectTriangle(line, triangle);

            // Assert
            Assert.Equal(expectedIntersection != null, result.HasIntersection);
            Assert.Equal(expectedIntersection, result.IntersectionPoint);
        }
    }
}
