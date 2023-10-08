
using RayTracing.Math.Calculations;
using System.Numerics;

namespace RayTracing.Math.Tests.Calculations
{
    public class VectorCalculator3DTests
    {
        [Fact]
        public void Intersect_ZLineThroughXyPlane_ReturnsExpectedIntersection()
        {
            this.Intersect_GivenLineAndPlane_ReturnsExpectedResult(
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
        public void Intersect_AngledLineThroughTiltedPlane_ReturnsExpectedIntersection()
        {
            this.Intersect_GivenLineAndPlane_ReturnsExpectedResult(
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
        public void Intersect_LineParallelToPlane_ReturnsNull()
        {
            this.Intersect_GivenLineAndPlane_ReturnsExpectedResult(
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
            this.IntersectTriangle_GivenLineAndTriangle_ReturnsExpectedResult(
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
        public void IntersectTriangle_ZLineThroughCornerA_ReturnsExpectedIntersection()
        {
            this.IntersectTriangle_GivenLineAndTriangle_ReturnsExpectedResult(
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
            this.IntersectTriangle_GivenLineAndTriangle_ReturnsExpectedResult(
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
            this.IntersectTriangle_GivenLineAndTriangle_ReturnsExpectedResult(
                new Line3D(
                    pointA: new Vector3(2, 4, -5),
                    pointB: new Vector3(2, 4, -6)),
                new Triangle3D(
                    cornerA: new Vector3(2, 2, -3),
                    cornerB: new Vector3(4, 2, -3),
                    cornerC: new Vector3(2, 4, -3)),
                new Vector3(2, 4, -3));
        }

        private void Intersect_GivenLineAndPlane_ReturnsExpectedResult(
            Line3D line,
            Plane3D plane,
            Vector3? expectedIntersection)
        {
            // Act
            var actualIntersection = VectorCalculator3D.Intersect(line, plane);

            // Assert
            Assert.Equal(expectedIntersection, actualIntersection);
        }

        private void IntersectTriangle_GivenLineAndTriangle_ReturnsExpectedResult(
            Line3D line,
            Triangle3D triangle,
            Vector3? expectedIntersection)
        {
            // Act
            var actualIntersection = VectorCalculator3D.IntersectTriangle(line, triangle);

            // Assert
            Assert.Equal(expectedIntersection, actualIntersection);
        }
    }
}
