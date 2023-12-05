using System.Xml.Serialization;

namespace RayTracing.ModelFiles.ColladaFormat.Xml
{
    [XmlType("light")]
    public class Light
    {
        [XmlAttribute("id")]
        public string? Id { get; set; }

        [XmlAttribute("name")]
        public string? Name { get; set; }

        [XmlElement("triangles")]
        public LightTechniqueCommon? TechniqueCommon { get; set; }
    }
}
