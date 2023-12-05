using System.Xml.Serialization;

namespace RayTracing.ModelFiles.ColladaFormat.Xml.Geometries
{
    [XmlType("geometry")]
    public class Geometry
    {
        [XmlAttribute("id")]
        public string? Id { get; set; }

        [XmlAttribute("name")]
        public string? Name { get; set; }

        [XmlElement("mesh")]
        public Mesh? Mesh { get; set; }
    }
}
