
using System.Xml.Serialization;

namespace RayTracing.ModelFiles.ColladaFormat.Xml
{
    [XmlType("light_properties")]
    public class LightProperties
    {
        [XmlElement("color")]
        public string? ColorString { get; set; }

        [XmlElement("falloff_angle")]
        public string? FalloffAngleString { get; set; }
    }
}
