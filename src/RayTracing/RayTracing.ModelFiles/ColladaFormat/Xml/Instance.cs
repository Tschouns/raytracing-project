using System.Xml.Serialization;

namespace RayTracing.ModelFiles.ColladaFormat.Xml
{
    [XmlType("instance")]
    public class Instance
    {
        [XmlAttribute("url")]
        public string? Url { get; set; }
    }
}
