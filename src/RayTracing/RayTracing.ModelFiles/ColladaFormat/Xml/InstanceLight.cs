using System.Xml.Serialization;

namespace RayTracing.ModelFiles.ColladaFormat.Xml
{
    [XmlType("instance_light")]
    public class InstanceLight
    {
        [XmlAttribute("url")]
        public string? Url { get; set; }
    }
}
