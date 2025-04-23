using RayTracing.Base;
using RayTracing.Math;
using RayTracing.Model;
using RayTracing.Model.Octrees;
using RayTracing.ModelFiles.ColladaFormat.Xml;
using RayTracing.ModelFiles.ColladaFormat.Xml.Effects;
using RayTracing.ModelFiles.ColladaFormat.Xml.Geometries;
using RayTracing.ModelFiles.ColladaFormat.Xml.Lights;
using System.Drawing;
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

            // Process materials.
            IDictionary<string, Material> materialsById = new Dictionary<string, Material>();
            
            foreach (var xmlMaterial in colladaRoot.LibraryMaterials)
            {
                var material = PrepMaterial(xmlMaterial, colladaRoot.LibraryEffects);
                materialsById.Add(xmlMaterial.Id, material);
            }

            // Process geometries.
            var geometries = new List<Model.Geometry>();

            foreach (var node in scene.SceneNodes.Where(n => n?.InstanceGeometry != null))
            {
                if (!node.InstanceGeometry!.Url.StartsWith('#'))
                {
                    throw new ArgumentException($"The instance geometry URL \"{node.InstanceGeometry.Url}\" is not supported.");
                }

                var geometryUrl = node.InstanceGeometry.Url.Substring(1);
                var xmlGeometry = colladaRoot.LibraryGeometries.Single(g => g.Id == geometryUrl);
                var transform = PrepTransformMatrix(node.MatrixString);
                var material = materialsById[xmlGeometry.Mesh.Triangles.MaterialId];
                
                var modelGeometry = PrepGeometry(xmlGeometry, transform, material);
                geometries.Add(modelGeometry);
            }

            // Process lights.
            var lightSources = new List<Model.LightSource>();

            foreach (var node in scene.SceneNodes.Where(n => n?.InstanceLight != null))
            {
                if (!node.InstanceLight!.Url.StartsWith('#'))
                {
                    throw new ArgumentException($"The instance light source URL \"{node.InstanceLight.Url}\" is not supported.");
                }

                var lightUrl = node.InstanceLight.Url.Substring(1);
                var xmlLight = colladaRoot.LibraryLights.Single(g => g.Id == lightUrl);
                var transform = PrepTransformMatrix(node.MatrixString);

                var modelLightSource = PrepLightSource(xmlLight, transform);
                lightSources.Add(modelLightSource);
            }

            var allMaterials = geometries
                .Select(g => g.Material)
                .Distinct()
                .ToList();

            var octree = OctreeHelper.PrepareOctree(geometries.SelectMany(g => g.Faces).ToList());

            return new Scene(allMaterials, geometries, octree, lightSources);
        }

        private static Material PrepMaterial(Xml.Materials.Material xmlMaterial, IEnumerable<Effect> xmlEffects)
        {
            Argument.AssertNotNull(xmlMaterial, nameof(xmlMaterial));
            Argument.AssertNotNull(xmlEffects, nameof(xmlEffects));
            
            if (!xmlMaterial.InstanceEffect.Url.StartsWith('#'))
            {
                throw new ArgumentException($"The instance effect URL \"{xmlMaterial.InstanceEffect.Url}\" is not supported.");
            }

            var effectId = xmlMaterial.InstanceEffect.Url.Substring(1);
            var effect = xmlEffects.Single(e => e.Id == effectId);

            var properties = effect.ProfileCommon?.Technique?.Lambert;
            if (properties == null)
            {
                throw new ArgumentException("Shader properties are missing.");
            }

            var colorString = properties.Diffuse?.ColorStringWithAlpha;
            var color = GetColorFromColorStringWithAlpha(colorString);

            var reflectivityString = properties.Reflectivity?.FloatValueString;
            var reflectivity = reflectivityString == null ? 0.5f : float.Parse(reflectivityString);

            var indexOfRefractionString = properties.IndexOfRefraction?.FloatValueString;
            var indexOfRefraction = indexOfRefractionString == null ? 1.3f : float.Parse(indexOfRefractionString);

            return new Material(
                xmlMaterial.Name,
                color.ToArgbColor(),
                reflectivity,
                glossyness: 0.5f, // TODO: how to get?
                transparency: 0f, // TODO: how to get?
                indexOfRefraction);
        }

        private static LightSource PrepLightSource(Light xmlLight, Matrix4x4 transform)
        {
            Argument.AssertNotNull(xmlLight, nameof(xmlLight));

            var location = transform.ApplyTo(new Vector3(0, 0, 0));

            var colorString = GetColorString(xmlLight);
            if (colorString == null)
            {
                throw new ArgumentException("No color found.", nameof(xmlLight));
            }

            var color = GetColorFromColorStringTriple(colorString);

            Spot? spot = null;
            if (xmlLight.TechniqueCommon?.Spot != null)
            {
                var pointingDirection = transform.ApplyTo(-Vector3.Up);
                var falloffAngle = float.Parse(xmlLight.TechniqueCommon.Spot.FalloffAngleString);
                spot = new Spot(pointingDirection, falloffAngle);
            }

            return new LightSource(xmlLight.Name, location, color.ToArgbColor(), spot);
        }

        private static string? GetColorString(Light light)
        {
            Argument.AssertNotNull(light, nameof(light));

            if (light.TechniqueCommon?.Point != null)
            {
                return light.TechniqueCommon.Point.ColorString0To1000;
            }

            if (light.TechniqueCommon?.Spot != null)
            {
                return light.TechniqueCommon.Spot.ColorString0To1000;
            }

            return null;
        }

        private static Model.Geometry PrepGeometry(
            Xml.Geometries.Geometry xmlGeometry,
            Matrix4x4 transform,
            Material material)
        {
            Argument.AssertNotNull(xmlGeometry, nameof(xmlGeometry));
            Argument.AssertNotNull(xmlGeometry.Mesh, nameof(xmlGeometry.Mesh));
            Argument.AssertNotNull(material, nameof(material));

            var mesh = xmlGeometry.Mesh;

            // Process vertices to faces.
            var vertices = GetVertices(mesh.Vertices, mesh.Sources);
            var verticesTransformed = vertices.Select(transform.ApplyTo).ToList();

            var indexStrings = mesh.Triangles.IndexesString.Split(' ');
            var indexes = indexStrings.Select(s => int.Parse(s)).ToList();

            var nInputs = mesh.Triangles.Inputs.Count();
            var vertexInput = mesh.Triangles.Inputs.Single(i => i.Semantic == "VERTEX");
            var vertexOffset = int.Parse(vertexInput.Offset);

            var vertexIndexes = indexes.Where((index, i) => i % nInputs == vertexOffset).ToList();
            var faces = new List<Face>();

            var geometry = new Model.Geometry(xmlGeometry.Name, material, faces);

            for (var i = 0; i + 2 < vertexIndexes.Count; i = i + 3)
            {
                var a = verticesTransformed[vertexIndexes[i]];
                var b = verticesTransformed[vertexIndexes[i + 1]];
                var c = verticesTransformed[vertexIndexes[i + 2]];

                var triangle = new Triangle3D(a, b, c);
                var normal = ((b - a).Cross(c - a)).Norm() ?? new Vector3(1,0,0);

                faces.Add(new Face(geometry, triangle, normal));
            }

            return geometry;
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

        private static Color GetColorFromColorStringTriple(string colorString)
        {
            Argument.AssertNotNull(colorString, nameof(colorString));

            var splits = colorString.Split(' ').Select(s => s.Trim()).ToArray();
            if (splits.Length != 3)
            {
                throw new ArgumentException("A color string must have 3 components.");
            }

            var scale = 1 / 1000f;
            var color = Color.FromArgb(
                red: GetColorValue(splits[0], scale),
                green: GetColorValue(splits[1], scale),
                blue: GetColorValue(splits[2], scale));

            return color;
        }

        private static Color GetColorFromColorStringWithAlpha(string? colorString)
        {
            if (colorString == null)
            {
                return Color.White;
            }

            var splits = colorString.Split(' ').Select(s => s.Trim()).ToArray();
            if (splits.Length != 4)
            {
                throw new ArgumentException("A color string must have 4 components.");
            }

            var color = Color.FromArgb(
                alpha: GetColorValue(splits[3], 1),
                red: GetColorValue(splits[0], 1),
                green: GetColorValue(splits[1], 1),
                blue: GetColorValue(splits[2], 1));

            return color;
        }

        private static int GetColorValue(string valueString, float scale)
        {
            Argument.AssertNotNull(valueString, nameof(valueString));

            var valueCollada = float.Parse(valueString);
            var valueColor = valueCollada * scale * 255;
            var valueInt = Convert.ToInt32(valueColor);

            return valueInt;
        }
    }
}
