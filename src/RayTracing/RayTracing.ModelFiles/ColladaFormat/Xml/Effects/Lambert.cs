using System.Xml.Serialization;

namespace RayTracing.ModelFiles.ColladaFormat.Xml.Effects
{
    [XmlType("lambert")]
    public class Lambert
    {
        [XmlElement("diffuse")]
        public ColorProperty? Diffuse { get; set; }

        [XmlElement("reflectivity")]
        public FloatProperty? Reflectivity { get; set; }

        [XmlElement("index_of_refraction")]
        public FloatProperty? IndexOfRefraction { get; set; }
    }
}
