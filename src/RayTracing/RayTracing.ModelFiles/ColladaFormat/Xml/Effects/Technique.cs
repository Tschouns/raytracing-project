
using System.Xml.Serialization;

namespace RayTracing.ModelFiles.ColladaFormat.Xml.Effects
{
    [XmlType("technique")]
    public class Technique
    {
        [XmlElement("lambert")]
        public Lambert? Lambert { get; set; }
    }
}
