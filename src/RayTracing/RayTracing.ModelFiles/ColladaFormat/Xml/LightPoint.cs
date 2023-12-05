
using System.Xml.Serialization;

namespace RayTracing.ModelFiles.ColladaFormat.Xml
{
    [XmlType("point")]
    public class LightPoint
    {
        [XmlElement("color")]
        public string? ColorString { get; set; }
    }
}
