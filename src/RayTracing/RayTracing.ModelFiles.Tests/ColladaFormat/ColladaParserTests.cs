using RayTracing.ModelFiles.ColladaFormat;
using RayTracing.ModelFiles.Tests.TestData;
using RayTracing.ModelFiles.Tests.TestTools;

namespace RayTracing.ModelFiles.Tests.ColladaFormat
{
    public class ColladaParserTests
    {
        [Fact]
        public void LoadFromFile_DummyExampleColladaFile_ReturnsExpectedModel()
        {
            // Arrange
            var candidate = new ColladaFileParser();

            using var workingDir = new TempDirectory();
            var filePath = Path.Combine(workingDir.FullName, "test.dae");
            File.WriteAllText(filePath, TestFiles.DummyDae);

            // Act
            var scene = candidate.LoadFromFile(filePath);

            // Assert
            Assert.NotNull(scene);

            var cone = scene.Geometries.Single(g => g.Name == "Cone");
            var cube = scene.Geometries.Single(g => g.Name == "Cube");
            var cylinder = scene.Geometries.Single(g => g.Name == "Cylinder");
            var plane = scene.Geometries.Single(g => g.Name == "Plane");

            Assert.Equal(62, cone.Faces.Count);
            Assert.Equal(12, cube.Faces.Count);
            Assert.Equal(124, cylinder.Faces.Count);
            Assert.Equal(2, plane.Faces.Count);

            var pointLight = scene.LightSources.Single(l => l.Name == "Light");
            var spotLight = scene.LightSources.Single(l => l.Name == "Spot");

            Assert.Null(pointLight.Spot);
            Assert.NotNull(spotLight.Spot);
        }

        [Fact]
        public void LoadFromFile_GlassExampleColladaFile_ReturnsExpectedModel()
        {
            // Arrange
            var candidate = new ColladaFileParser();

            using var workingDir = new TempDirectory();
            var filePath = Path.Combine(workingDir.FullName, "test.dae");
            File.WriteAllText(filePath, TestFiles.GlassDae);

            // Act
            var scene = candidate.LoadFromFile(filePath);

            // Assert
            Assert.NotNull(scene);

            var tablePlane = scene.Geometries.Single(g => g.Name == "Plane");
            var glass = scene.Geometries.Single(g => g.Name == "Cylinder");

            Assert.Equal(2, tablePlane.Faces.Count);
            Assert.Equal(3712, glass.Faces.Count);

            var pointLight = scene.LightSources.Single(l => l.Name == "Light");

            Assert.Null(pointLight.Spot);
        }
    }
}