
using RayTracing.Math.Calculations;

namespace RayTracing.Math.Tests.Calculations
{
    public class AabbHelperTests
    {
        [Fact]
        public void PrepareOctants_GivenParentBox_ReturnsEightOctants()
        {
            // Arrange
            var box = new AxisAlignedBoundingBox(
                min: new Vector3(0, 0, 0),
                max: new Vector3(2, 4, 6));

            // Act
            var octants = AabbHelper.PrepareOctants(box);

            // Assert
            Assert.Equal(8, octants.Count());
            Assert.Equal(box.Min, octants.First().Min);
            Assert.Equal(box.Max, octants.Last().Max);
        }

        [Fact]
        public void IntersectAabb_RayHitsUpwards_ReturnsTrue()
        {
            this.IntersectAabb_GivenRayWithAabb_ReturnsExpectedResult(
                origin: new Vector3(1, 1, 1),
                direction: new Vector3(2, 2, 2),
                aabbMin: new Vector3(3, 3, 3),
                aabbMax: new Vector3(4, 4, 4),
                expectedResult: true);
        }

        [Fact]
        public void IntersectAabb_RayHitsDownwards_ReturnsTrue()
        {
            this.IntersectAabb_GivenRayWithAabb_ReturnsExpectedResult(
                origin: new Vector3(4, 4, 4),
                direction: new Vector3(3, 3, 3),
                aabbMin: new Vector3(1, 1, 1),
                aabbMax: new Vector3(2, 2, 2),
                expectedResult: true);
        }

        [Fact]
        public void IntersectAabb_RayStartsWithinBox_ReturnsTrue()
        {
            this.IntersectAabb_GivenRayWithAabb_ReturnsExpectedResult(
                origin: new Vector3(1.5f, 1.5f, 1.5f),
                direction: new Vector3(3, 3, 3),
                aabbMin: new Vector3(1, 1, 1),
                aabbMax: new Vector3(2, 2, 2),
                expectedResult: true);
        }

        [Fact]
        public void IntersectAabb_RayMissesForwardToTheLeft_ReturnsFalse()
        {
            this.IntersectAabb_GivenRayWithAabb_ReturnsExpectedResult(
                origin: new Vector3(-1.01f, 1, -5),
                direction: new Vector3(0, 0, 2),
                aabbMin: new Vector3(-1, -1, -1),
                aabbMax: new Vector3(1, 2, 2),
                expectedResult: false);
        }

        [Fact]
        public void IntersectAabb_RayMissesForwardToTheRight_ReturnsFalse()
        {
            this.IntersectAabb_GivenRayWithAabb_ReturnsExpectedResult(
                origin: new Vector3(3.01f, 1, -5),
                direction: new Vector3(0, 0, 2),
                aabbMin: new Vector3(-1, -1, -1),
                aabbMax: new Vector3(3, 2, 2),
                expectedResult: false);
        }

        private void IntersectAabb_GivenRayWithAabb_ReturnsExpectedResult(
            Vector3 origin,
            Vector3 direction,
            Vector3 aabbMin,
            Vector3 aabbMax,
            bool expectedResult)
        {
            // Arrange

            // Act
            var result = AabbHelper.IntersectAabb(origin, direction, aabbMin, aabbMax);

            // Assert
            Assert.Equal(expectedResult, result.DoIntersect);
        }
    }
}
