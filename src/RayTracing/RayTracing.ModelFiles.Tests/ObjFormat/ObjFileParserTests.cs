using RayTracing.ModelFiles.ObjFormat;
using RayTracing.ModelFiles.Tests.TestData;
using RayTracing.ModelFiles.Tests.TestTools;

namespace RayTracing.ModelFiles.Tests.ObjFormat;

public class ObjFileParserTests
{
    [Fact]
    public void LoadFromFile_ExampleObjFile_ReturnsExpectedModel()
    {
        // Arrange
        var candidate = new ObjFileParser();

        using var workingDir = new TempDirectory();
        var filePath = Path.Combine(workingDir.FullName, "test.obj");
        File.WriteAllText(filePath, TestFiles.IndoorPlantObj);

        // Act
        var scene = candidate.LoadFromFile(filePath);

        // Assert
        Assert.NotNull(scene);
        Assert.Equal(45840, scene.Geometries.First().Faces.Count);
    }
}