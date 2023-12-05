
using System.Xml.Serialization;

namespace RayTracing.ModelFiles.ColladaFormat.Xml
{
    [XmlType("spot")]
    public class LightSpot
    {
        [XmlElement("color")]
        public string? ColorString { get; set; }
    }
}
