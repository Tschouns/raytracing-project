
using System.Xml.Serialization;

namespace RayTracing.ModelFiles.ColladaFormat.Xml.Effects
{
    [XmlType("effect")]
    public class Effect
    {
        [XmlElement("profile_COMMON")]
        public ProfileCommon? ProfileCommon { get; set; }
    }
}
