
using System.Xml.Serialization;

namespace RayTracing.ModelFiles.ColladaFormat.Xml.Geometries
{
    [XmlType("triangles")]
    public class MeshTriangles
    {
        [XmlAttribute("material")]
        public string? MaterialId { get; set; }

        [XmlAttribute("count")]
        public int Count { get; set; }

        [XmlElement("input")]
        public MeshInput[]? Inputs { get; set; }

        [XmlElement("p")]
        public string? IndexesString { get; set; }
    }
}
