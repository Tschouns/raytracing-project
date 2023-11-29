using System.Xml.Serialization;

namespace RayTracing.ModelFiles.ColladaFormat.Xml.Effects
{
    [XmlType("float_property")]
    public class FloatProperty
    {
        [XmlElement("float")]
        public string? FloatValueString { get; set; }
    }
}
