
namespace RayTracing.Math.Tests
{
    public class Matrix4x4Tests
    {
        private readonly float t = 0.0000001f; // tolerance
        private readonly float degreesToRad = MathF.PI / 180f;

        [Fact]
        public void Add_ToOtherMatrix_ReturnsExpectedResult()
        {
            // Arrange
            var a = CreateA();
            var b = CreateB();

            var expected = new Matrix4x4(
                60, 62, 64, 66,
                80, 82, 84, 86,
                100, 102, 104, 106,
                120, 122, 124, 126);

            // Act
            var actual = a.Add(b);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Multiply_ByOtherMatrix_ReturnsExpectedResult()
        {
            // Arrange
            var a = CreateA();
            var b = CreateB();

            var expected = new Matrix4x4(
                3040, 3086, 3132, 3178,
                5640, 5726, 5812, 5898,
                8240, 8366, 8492, 8618,
                10840, 11006, 11172, 11338);

            // Act
            var actual = a.Multiply(b);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Multiply_ByItentityMatrix_ReturnsSameMatrix()
        {
            // Arrange
            var m = CreateA();

            // Act
            var actual = m.Multiply(Matrix4x4.Identity);

            // Assert
            Assert.Equal(m, actual);
        }

        [Fact]
        public void Translate_Point_TransformsPointAsExpected()
        {
            // Arrange
            var p = new Vector3(2, 3, 4);
            var v = new Vector3(5, -6, 7);
            var pNewExpected = new Vector3(7, -3, 11);

            // Act
            var rotation = Matrix4x4.Translate(v);
            var pNewActual = rotation.ApplyTo(p);

            // Assert
            Assert.Equal(pNewExpected.X, pNewActual.X, this.t);
            Assert.Equal(pNewExpected.Y, pNewActual.Y, this.t);
            Assert.Equal(pNewExpected.Z, pNewActual.Z, this.t);
        }

        [Fact]
        public void RotateX_Point_TransformsPointAsExpected()
        {
            // Arrange
            var p = new Vector3(3, 0, 1);
            var pNewExpected = new Vector3(3, -1, 0);

            // Act
            var rotation = Matrix4x4.RotateX(90 * this.degreesToRad);
            var pNewActual = rotation.ApplyTo(p);

            // Assert
            Assert.Equal(pNewExpected.X, pNewActual.X, this.t);
            Assert.Equal(pNewExpected.Y, pNewActual.Y, this.t);
            Assert.Equal(pNewExpected.Z, pNewActual.Z, this.t);
        }

        [Fact]
        public void RotateY_Point_TransformsPointAsExpected()
        {
            // Arrange
            var p = new Vector3(0, 3, 1);
            var pNewExpected = new Vector3(1, 3, 0);

            // Act
            var rotation = Matrix4x4.RotateY(90 * this.degreesToRad);
            var pNewActual = rotation.ApplyTo(p);

            // Assert
            Assert.Equal(pNewExpected.X, pNewActual.X, this.t);
            Assert.Equal(pNewExpected.Y, pNewActual.Y, this.t);
            Assert.Equal(pNewExpected.Z, pNewActual.Z, this.t);
        }

        [Fact]
        public void RotateZ_Point_TransformsPointAsExpected()
        {
            // Arrange
            var p = new Vector3(0, 1, 3);
            var pNewExpected = new Vector3(-1, 0, 3);

            // Act
            var rotation = Matrix4x4.RotateZ(90 * this.degreesToRad);
            var pNewActual = rotation.ApplyTo(p);

            // Assert
            Assert.Equal(pNewExpected.X, pNewActual.X, this.t);
            Assert.Equal(pNewExpected.Y, pNewActual.Y, this.t);
            Assert.Equal(pNewExpected.Z, pNewActual.Z, this.t);
        }

        [Fact]
        public void Multiply_ByItentityMatrixFirst_ReturnsSameMatrix()
        {
            // Arrange
            var m = CreateA();

            // Act
            var actual = Matrix4x4.Identity.Multiply(m);

            // Assert
            Assert.Equal(m, actual);
        }

        private static Matrix4x4 CreateA()
        {
            return new Matrix4x4(
                10, 11, 12, 13,
                20, 21, 22, 23,
                30, 31, 32, 33,
                40, 41, 42, 43);
        }

        private static Matrix4x4 CreateB()
        {
            return new Matrix4x4(
                50, 51, 52, 53,
                60, 61, 62, 63,
                70, 71, 72, 73,
                80, 81, 82, 83);
        }
    }
}
