using System.Xml.Serialization;

namespace RayTracing.ModelFiles.ColladaFormat.Xml.Effects
{
    [XmlType("lambert")]
    public class Lambert
    {
        [XmlElement("diffuse")]
        public Diffuse? Diffuse { get; set; }
    }
}
