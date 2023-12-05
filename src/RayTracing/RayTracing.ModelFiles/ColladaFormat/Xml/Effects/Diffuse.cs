using System.Xml.Serialization;

namespace RayTracing.ModelFiles.ColladaFormat.Xml.Effects
{
    [XmlType("diffuse")]
    public class Diffuse
    {
        [XmlElement("color")]
        public string? ColorString { get; set; }
    }
}
