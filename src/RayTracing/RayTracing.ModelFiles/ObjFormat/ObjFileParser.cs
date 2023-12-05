using RayTracing.Base;
using RayTracing.Math;
using RayTracing.Model;

namespace RayTracing.ModelFiles.ObjFormat
{
    /// <summary>
    /// OBJ implementation of <see cref="IModelFileParser"/>.
    /// </summary>
    public class ObjFileParser : IModelFileParser
    {
        public Scene LoadFromFile(string fileName)
        {
            Argument.AssertNotNull(fileName, nameof(fileName));

            var objData = new ObjData();
            var lines = File.ReadAllLines(fileName);
            var lineNumber = 1;

            foreach (var line in lines)
            {
                try
                {
                    ProcessLine(line, objData);
                }
                catch (Exception ex)
                {
                    throw new ArgumentException($"There was an error on line {lineNumber}: {ex.Message}", ex);
                }

                lineNumber++;
            }

            var faces = objData.Triangles.Select(t => new Face(t, new Vector3())).ToList();
            var geometry = new Geometry("unknown", faces);

            return new Scene(new List<Geometry> { geometry }, new List<LightSource>());
        }

        private void ProcessLine(string line, ObjData data)
        {
            var splits = line.Split(' ');
            var start = splits[0];
            var remainingStrings = splits.Skip(1).ToArray();

            switch (start)
            {
                case "v":
                    this.ParseVertex(remainingStrings, data);
                    break;
                case "f":
                    this.ParseFace(remainingStrings, data);
                    break;
                default:
                    break;
            }
        }

        private void ParseVertex(string[] strings, ObjData data)
        {
            var v = new Vector3(
                float.Parse(strings[0]),
                float.Parse(strings[1]),
                float.Parse(strings[2]));

            data.Vertex.Add(v);
        }

        private void ParseFace(string[] strings, ObjData data)
        {
            var center = GetVertexIndex(strings.First());

            for (var i = 1; i <= strings.Length - 2; i++)
            {
                var aIndex = center;
                var bIndex = GetVertexIndex(strings[i]);
                var cIndex = GetVertexIndex(strings[i + 1]);

                var a = data.Vertex[aIndex];
                var b = data.Vertex[bIndex];
                var c = data.Vertex[cIndex];

                var f = new Triangle3D(a, b, c);

                data.Triangles.Add(f);
            }
        }

        private static int GetVertexIndex(string vertexRef)
        {
            var indexString = vertexRef.Split('/').First();
            var index = int.Parse(indexString);

            // OBJ idexes start at 1.
            return index - 1;
        }

        private class ObjData
        {
            public IList<Vector3> Vertex { get; } = new List<Vector3>();
            public IList<Triangle3D> Triangles { get; } = new List<Triangle3D>();
        }
    }
}
