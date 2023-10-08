using RayTracing.Math;

namespace RayTracing.Rendering.Tests
{
    public class CameraTests
    {
        [Fact]
        public void GetRasterRays_GivenSettings_ReturnsRayForEachPixel()
        {
            // Arrange
            ushort xRes = 10;
            ushort yRes = 8;

            var candidate = new Camera(
                size: new Vector2(0.1f, 0.8f),
                horizontalResolution: xRes,
                verticalResolution: yRes,
                focalLength: 0.5f,
                position: new Vector3(0, 0, 0),
                lookingDirection: new Vector3(0, 0, 1));

            // Act
            var rays = candidate.GeneratePixelRays();

            // Assert
            Assert.Equal(xRes * yRes, rays.Count());
            for (var x = 0; x < xRes; x++)
            {
                for (var y = 0; y < yRes; y++)
                {
                    var ray = rays.Single(r => r.X == x && r.Y == y);
                }
            }
        }
    }
}