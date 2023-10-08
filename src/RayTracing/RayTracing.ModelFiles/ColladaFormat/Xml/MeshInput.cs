using System.Xml.Serialization;

namespace RayTracing.ModelFiles.ColladaFormat.Xml
{
    [XmlType("input")]
    public class MeshInput
    {
        [XmlAttribute("semantic")]
        public string? Semantic { get; set; }

        [XmlAttribute("source")]
        public string? Source { get; set; }

        [XmlAttribute("offset")]
        public string? Offset { get; set; }
    }
}
