
using System.Xml.Serialization;

namespace RayTracing.ModelFiles.ColladaFormat.Xml
{
    [XmlType("triangles")]
    public class MeshTriangles
    {
        [XmlAttribute("count")]
        public int Count { get; set; }

        [XmlElement("input")]
        public MeshInput[]? Inputs { get; set; }

        [XmlElement("p")]
        public string? IndexesString { get; set; }
    }
}
