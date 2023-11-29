using System.Xml.Serialization;

namespace RayTracing.ModelFiles.ColladaFormat.Xml.Geometries
{
    [XmlType("mesh")]
    public class Mesh
    {
        [XmlElement("source")]
        public MeshSource[]? Sources { get; set; }

        [XmlElement("vertices")]
        public MeshVertices? Vertices { get; set; }

        [XmlElement("triangles")]
        public MeshTriangles? Triangles { get; set; }
    }
}
