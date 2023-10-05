using RayTracing.Math.Calculations;

namespace RayTracing.Math.Tests.Calculations
{
    public class VectorCalculator2DTests
    {
        [Fact]
        public void Intersect_45DegLeftRight_ReturnsExpectedResult()
        {
            this.Intersect_GivenLines_ReturnsExpectedResult(
                new Line2D(0, 0, 1, 1),
                new Line2D(0, 1, 1, 0),
                new Vector2(0.5f, 0.5f));
        }

        [Fact]
        public void Intersect_45DegRightLeft_ReturnsExpectedResult()
        {
            this.Intersect_GivenLines_ReturnsExpectedResult(
                new Line2D(1, 1, 0, 0),
                new Line2D(1, 0, 0, 1),
                new Vector2(0.5f, 0.5f));
        }

        [Fact]
        public void Intersect_90Deg_ReturnsExpectedResult()
        {
            this.Intersect_GivenLines_ReturnsExpectedResult(
                new Line2D(0, 0, 1, 0),
                new Line2D(0, -1, 1, 1),
                new Vector2(0.5f, 0));
        }

        [Fact]
        public void Intersect_NegativeSpace_ReturnsExpectedResult()
        {
            this.Intersect_GivenLines_ReturnsExpectedResult(
                new Line2D(-1, -1, -8, -1),
                new Line2D(-3, -4, -3, 5),
                new Vector2(-3, -1));
        }

        [Fact]
        public void Intersect_ParallelLines_ReturnsExpectedResult()
        {
            this.Intersect_GivenLines_ReturnsExpectedResult(
                new Line2D(1, 1, 2, 2),
                new Line2D(1, 3, 2, 4),
                null);
        }

        private void Intersect_GivenLines_ReturnsExpectedResult(
            Line2D line1,
            Line2D line2,
            Vector2? expectedIntersection)
        {
            // Act
            var actualIntersection = VectorCalculator2D.Intersect(line1, line2);

            // Assert
            Assert.Equal(expectedIntersection, actualIntersection);
        }
    }
}