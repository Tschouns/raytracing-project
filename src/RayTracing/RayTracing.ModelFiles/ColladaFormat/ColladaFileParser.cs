using RayTracing.Base;
using RayTracing.Math;
using RayTracing.Model;
using RayTracing.ModelFiles.ColladaFormat.Xml;
using System.Xml.Serialization;

namespace RayTracing.ModelFiles.ColladaFormat
{
    /// <summary>
    /// Collada implementation of <see cref="IModelFileParser"/>.
    /// </summary>
    public class ColladaFileParser : IModelFileParser
    {
        public Scene LoadFromFile(string fileName)
        {
            Argument.AssertNotNull(fileName, nameof(fileName));

            using var fileStreamReader = File.OpenText(fileName);

            var serializer = new XmlSerializer(typeof(ColladaRoot));
            var colladaRoot = serializer.Deserialize(fileStreamReader) as ColladaRoot;

            if (colladaRoot == null)
            {
                throw new ArgumentException("The specified file could not be deserialized.");
            }

            var scene = colladaRoot.LibraryVisualScenes.Single();
            var geometries = new List<Model.Geometry>();

            foreach (var node in scene.SceneNodes.Where(n => n?.InstanceGeometry != null))
            {
                if (!node.InstanceGeometry.Url.StartsWith('#'))
                {
                    throw new ArgumentException($"The instance geometry URL \"{node.InstanceGeometry.Url}\" is not supported.");
                }

                var geometryUrl = node.InstanceGeometry.Url.Substring(1);
                var xmlGeometry = colladaRoot.LibraryGeometries.Single(g => g.Id == geometryUrl);
                var transform = PrepTransformMatrix(node.MatrixString);

                var modelGeometry = PrepGeometry(xmlGeometry, transform);
                geometries.Add(modelGeometry);
            }

            return new Scene(geometries);
        }

        private static Matrix4x4 PrepTransformMatrix(string matrixValues)
        {
            Argument.AssertNotNull(matrixValues, nameof(matrixValues));

            var valuesArray = matrixValues.Split(' ');
            if (valuesArray.Length != 16)
            {
                throw new ArgumentException("The matrix must have 16 values.");
            }

            var m = valuesArray.Select(float.Parse).ToArray();

            return new Matrix4x4(
                m[0], m[1], m[2], m[3],
                m[4], m[5], m[6], m[7],
                m[8], m[9], m[10], m[11],
                m[12], m[13], m[14], m[15]);
        }

        private static Model.Geometry PrepGeometry(Xml.Geometry xmlGeometry, Matrix4x4 transform)
        {
            Argument.AssertNotNull(xmlGeometry, nameof(xmlGeometry));
            Argument.AssertNotNull(xmlGeometry.Mesh, nameof(xmlGeometry.Mesh));
            
            var mesh = xmlGeometry.Mesh;

            var vertices = GetVertices(mesh.Vertices, mesh.Sources);
            var verticesTransformed = vertices.Select(transform.ApplyTo).ToList();

            var indexStrings = mesh.Triangles.IndexesString.Split(' ');
            var indexes = indexStrings.Select(s => int.Parse(s)).ToList();

            var nInputs = mesh.Triangles.Inputs.Count();
            var vertexInput = mesh.Triangles.Inputs.Single(i => i.Semantic == "VERTEX");
            var vertexOffset = int.Parse(vertexInput.Offset);

            var vertexIndexes = indexes.Where((index, i) => i % nInputs == vertexOffset).ToList();
            var faces = new List<Face>();

            for (var i = 0; i + 2 < vertexIndexes.Count; i = i + 3)
            {
                var triangle = new Triangle3D(
                    verticesTransformed[vertexIndexes[i]],
                    verticesTransformed[vertexIndexes[i + 1]],
                    verticesTransformed[vertexIndexes[i + 2]]);

                faces.Add(new Face(triangle, new Vector3()));
            }

            return new Model.Geometry(xmlGeometry.Name, faces);
        }

        private static IReadOnlyList<Vector3> GetVertices(MeshVertices meshVertices, IEnumerable<MeshSource> sources)
        {
            Argument.AssertNotNull(meshVertices, nameof(meshVertices));
            Argument.AssertNotNull(meshVertices.Input, nameof(meshVertices.Input));
            Argument.AssertNotNull(sources, nameof(sources));

            var verticesSourceId = meshVertices.Input.Source.Substring(1);
            var source = sources.SingleOrDefault(s => s.Id == verticesSourceId);
            if (source?.FloatArray == null)
            {
                throw new InvalidOperationException($"No source or no float array for \"{meshVertices.Input.Source}\".");
            }

            var scalarValues = source.FloatArray!.Split(' ')
                .Select(s => float.Parse(s))
                .ToList();

            var vertices = new List<Vector3>();

            for (var i = 0; i + 2 < scalarValues.Count; i = i + 3)
            {
                var vertex = new Vector3(
                    scalarValues[i],
                    scalarValues[i + 1],
                    scalarValues[i + 2]);

                vertices.Add(vertex);
            }

            return vertices;
        }
    }
}
