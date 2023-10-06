
using RayTracing.Math.Calculations;

namespace RayTracing.Math.Tests.Calculations
{
    public class VectorCalculator3DTests
    {
        [Fact]
        public void Intersect_ZLineThroughXyPlane_ReturnsExpectedIntersection()
        {
            this.Intersect_GivenLines_ReturnsExpectedResult(
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
            this.Intersect_GivenLines_ReturnsExpectedResult(
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
            this.Intersect_GivenLines_ReturnsExpectedResult(
                new Line3D(
                    pointA: new Vector3(7, 0, 1),
                    pointB: new Vector3(0, 3, 1)),
                new Plane3D(
                    pointP: new Vector3(0, 0, 0),
                    vectorU: new Vector3(3, 0, 0),
                    vectorV: new Vector3(0, 5, 0)),
                null);
        }

        private void Intersect_GivenLines_ReturnsExpectedResult(
            Line3D line,
            Plane3D plane,
            Vector3? expectedIntersection)
        {
            // Act
            var actualIntersection = VectorCalculator3D.Intersect(line, plane);

            // Assert
            Assert.Equal(expectedIntersection, actualIntersection);
        }
    }
}
