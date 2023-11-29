using System.Xml.Serialization;

namespace RayTracing.ModelFiles.ColladaFormat.Xml.Materials
{
    [XmlType("material")]
    public class Material
    {
        [XmlAttribute("id")]
        public string? Id { get; set; }

        [XmlAttribute("name")]
        public string? Name { get; set; }

        [XmlElement("instance_effect")]
        public Instance? InstanceEffect { get; set; }
    }
}
