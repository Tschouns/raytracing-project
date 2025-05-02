using System.Xml.Serialization;

namespace RayTracing.ModelFiles.ColladaFormat.Xml.Geometries
{
    [XmlType("source")]
    public class MeshSource
    {
        [XmlAttribute("id")]
        public string? Id { get; set; }

        [XmlElement("float_array")]
        public string? FloatArray { get; set; }
    }
}
