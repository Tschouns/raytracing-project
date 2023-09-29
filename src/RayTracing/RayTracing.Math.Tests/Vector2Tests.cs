using System;
using System.Collections.Generic;

namespace RayTracing.Math.Tests
{
    public class Vector2Tests
    {
        [Theory]
        [InlineData(1, 1, 1.4140f)]
        [InlineData(1, -1, 1.4140f)]
        [InlineData(-1, -1, 1.4140f)]
        public void Length_GivenValues_ReturnsExpectedResult(float x, float y, float expectedLength)
        {
            // Arrange
            var vector = new Vector2(x, y);

            // Act
            var length = vector.Length();

            // Assert
            Assert.Equal(expectedLength, length, 0.001f);
        }
    }
}
