﻿
namespace RayTracing.Math.Tests
{
    public class Vector3Tests
    {
        [Theory]
        [InlineData(0, 3, 4, 5)]
        [InlineData(1, 1, 1, 1.7320)]
        [InlineData(-1, -1, 1, 1.7320f)]
        [InlineData(1, -1, 0, 1.4140f)]
        [InlineData(0, 0, 0, 0f)]
        public void Length_GivenVector_ReturnsExpectedLength(float x, float y, float z, float expectedLength)
        {
            // Arrange
            var vector = new Vector3(x, y, z);

            // Act
            var length = vector.Length();

            // Assert
            Assert.Equal(expectedLength, length, 0.001f);
        }

        [Theory]
        [InlineData(1, 1, 1)]
        [InlineData(1, -1, 1)]
        [InlineData(-1, -1, 0)]
        [InlineData(37, -1676, 314)]
        public void Norm_GivenVector_ReturnsVectorWithSameRatioAndLength1(float x, float y, float z)
        {
            // Arrange
            var vector = new Vector3(x, y, z);

            // Act
            var norm = vector.Norm();

            // Assert
            Assert.NotNull(norm);
            Assert.Equal(1, norm.Value.Length(), 0.00001f);
            Assert.Equal(vector.X / vector.Y, norm.Value.X / norm.Value.Y, 0.00001f);
            Assert.Equal(vector.Y / vector.Z, norm.Value.Y / norm.Value.Z, 0.00001f);
        }

        [Fact]
        public void Norm_GivenZeroVector_ReturnsNull()
        {
            // Arrange
            var vector = new Vector3(0, 0, 0);

            // Act
            var norm = vector.Norm();

            // Assert
            Assert.Null(norm);
        }
    }
}