using System.Xml.Serialization;

namespace RayTracing.ModelFiles.ColladaFormat.Xml.Lights
{
    [XmlType("technique_common")]
    public class LightTechniqueCommon
    {
        [XmlElement("point")]
        public LightProperties? Point { get; set; }

        [XmlElement("spot")]
        public LightProperties? Spot { get; set; }
    }
}
