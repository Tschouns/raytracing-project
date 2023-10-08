using System.Xml.Serialization;

namespace RayTracing.ModelFiles.ColladaFormat.Xml
{
    [XmlType("instance_geometry")]
    public class InstanceGeometry
    {
        [XmlAttribute("url")]
        public string? Url { get; set; }
    }
}
