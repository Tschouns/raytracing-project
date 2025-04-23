
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
    }
}
