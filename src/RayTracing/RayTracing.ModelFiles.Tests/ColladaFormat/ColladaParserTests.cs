using RayTracing.ModelFiles.ColladaFormat;
using RayTracing.ModelFiles.Tests.TestData;
using RayTracing.ModelFiles.Tests.TestTools;

namespace RayTracing.ModelFiles.Tests.ColladaFormat;

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
        Assert.Equal(124, scene.Geometries[0].Faces.Count);
        Assert.Equal(12, scene.Geometries[1].Faces.Count);
    }
}
