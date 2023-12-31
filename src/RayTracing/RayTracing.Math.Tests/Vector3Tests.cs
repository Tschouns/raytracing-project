﻿
namespace RayTracing.Math.Tests
{
    public class Vector3Tests
    {
        private static readonly float tolerance = 0.00001f;

        [Fact]
        public void Dot_GivenPerpendicularVectors_Returns0()
        {
            // Arrange
            var a = new Vector3(1, 0, 0);
            var b = new Vector3(0, 1, 5);

            var expected = 0f;

            // Act
            var actual = a.Dot(b);

            // Assert
            Assert.Equal(expected, actual, tolerance);
        }

        [Fact]
        public void Dot_GivenIdenticalNormVectors_Returns1()
        {
            // Arrange
            var a = new Vector3(1, 6, 7).Norm()!.Value;

            var expected = 1f;

            // Act
            var actual = a.Dot(a);

            // Assert
            Assert.Equal(expected, actual, tolerance);
        }

        [Fact]
        public void Dot_FullLengthProjectionOntoNorm_Returns1()
        {
            // Arrange
            var a = new Vector3(1, 0, 0);
            var b = new Vector3(1, 3, 5);

            var expected = 1f;

            // Act
            var actual = a.Dot(b);

            // Assert
            Assert.Equal(expected, actual, tolerance);
        }

        [Fact]
        public void Dot_FullLengthProjectionOntoAnyA_ReturnsALengthSquared()
        {
            // Arrange
            var a = new Vector3(0, 4, 0);
            var b = new Vector3(1, 4, 5);

            var expected = a.LengthSquared();

            // Act
            var actual = a.Dot(b);

            // Assert
            Assert.Equal(expected, actual, tolerance);
        }

        [Fact]
        public void Dot_OpenAngle_ReturnsNegative()
        {
            // Arrange
            var a = new Vector3(3, 0, 0);
            var b = new Vector3(-2, 8, 0);

            var expected = -6f;

            // Act
            var actual = a.Dot(b);

            // Assert
            Assert.Equal(expected, actual, tolerance);
        }

        [Fact]
        public void Cross_GivenTwoAxisVectors_ReturnsRemainingAxisVector()
        {
            // Arrange
            var a = new Vector3(1, 0, 0);
            var b = new Vector3(0, 1, 0);

            var expected = new Vector3(0, 0, 1);

            // Act
            var actual = a.Cross(b);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Cross_GivenTwoArbitraryVectors_ReturnsPerpendicularVector()
        {
            // Arrange
            var a = new Vector3(1, 2, 3);
            var b = new Vector3(3, 4, 5);

            var expected = new Vector3(-2, 4, -2);

            // Act
            var actual = a.Cross(b);

            // Assert
            Assert.Equal(expected, actual);
        }

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
