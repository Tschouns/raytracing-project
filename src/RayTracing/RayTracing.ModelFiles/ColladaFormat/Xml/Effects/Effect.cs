
using System.Xml.Serialization;

namespace RayTracing.ModelFiles.ColladaFormat.Xml.Effects
{
    [XmlType("effect")]
    public class Effect
    {
        [XmlAttribute("id")]
        public string? Id { get; set; }

        [XmlElement("profile_COMMON")]
        public ProfileCommon? ProfileCommon { get; set; }
    }
}
