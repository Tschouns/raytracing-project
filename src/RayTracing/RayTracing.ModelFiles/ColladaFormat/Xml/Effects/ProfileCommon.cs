
using System.Xml.Serialization;

namespace RayTracing.ModelFiles.ColladaFormat.Xml.Effects
{
    [XmlType("profile_COMMON")]
    public class ProfileCommon
    {
        [XmlElement("technique")]
        public Technique? Technique { get; set; }
    }
}
