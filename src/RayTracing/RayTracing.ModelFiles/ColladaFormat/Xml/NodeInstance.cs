using System.Xml.Serialization;

namespace RayTracing.ModelFiles.ColladaFormat.Xml
{
    [XmlType("node_instance")]
    public class NodeInstance
    {
        [XmlAttribute("url")]
        public string? Url { get; set; }
    }
}
