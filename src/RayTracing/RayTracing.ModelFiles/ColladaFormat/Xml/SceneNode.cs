using System.Xml.Serialization;

namespace RayTracing.ModelFiles.ColladaFormat.Xml
{
    [XmlType("node")]
    public class SceneNode
    {
        [XmlAttribute("id")]
        public string? Id { get; set; }

        [XmlAttribute("name")]
        public string? Name { get; set; }

        [XmlAttribute("type")]
        public string? Type { get; set; }

        [XmlElement("matrix")]
        public string? MatrixString { get; set; }

        [XmlElement("instance_geometry")]
        public InstanceGeometry? InstanceGeometry { get; set; }
    }
}
