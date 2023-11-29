using System.Xml.Serialization;

namespace RayTracing.ModelFiles.ColladaFormat.Xml.Effects
{
    [XmlType("color_property")]
    public class ColorProperty
    {
        [XmlElement("color")]
        public string? ColorStringWithAlpha { get; set; }
    }
}
