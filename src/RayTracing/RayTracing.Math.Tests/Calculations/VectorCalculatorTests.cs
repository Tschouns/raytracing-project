using RayTracing.Math.Calculations;

namespace RayTracing.Math.Tests.Calculations
{
    public class VectorCalculatorTests
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

        private void Intersect_GivenLines_ReturnsExpectedResult(
            Line2D line1,
            Line2D line2,
            Vector2 expectedIntersection)
        {
            // Act
            var actualIntersection = VectorCalculator.Intersect(line1, line2);

            // Assert
            Assert.Equal(expectedIntersection, actualIntersection);
        }
    }
}