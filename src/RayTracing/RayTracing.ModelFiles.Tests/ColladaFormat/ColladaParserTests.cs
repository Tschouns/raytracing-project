using RayTracing.ModelFiles.ColladaFormat;
using RayTracing.ModelFiles.Tests.TestData;
using RayTracing.ModelFiles.Tests.TestTools;

namespace RayTracing.ModelFiles.Tests.ColladaFormat
{
    public class ColladaParserTests
    {
        [Fact]
        public void LoadFromFile_ExampleColladaFile_ReturnsExpectedModel()
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
    }
}