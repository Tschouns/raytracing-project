
using System.Xml.Serialization;

namespace RayTracing.ModelFiles.ColladaFormat.Xml.Lights
{
    [XmlType("light_properties")]
    public class LightProperties
    {
        [XmlElement("color")]
        public string? ColorString0To1000 { get; set; }

        [XmlElement("falloff_angle")]
        public string? FalloffAngleString { get; set; }
    }
}
