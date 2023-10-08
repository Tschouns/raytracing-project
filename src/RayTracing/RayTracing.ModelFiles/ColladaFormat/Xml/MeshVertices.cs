using System.Xml.Serialization;

namespace RayTracing.ModelFiles.ColladaFormat.Xml
{
    [XmlType("vertices")]
    public class MeshVertices
    {
        [XmlElement("input")]
        public MeshInput? Input { get; set; }
    }
}
