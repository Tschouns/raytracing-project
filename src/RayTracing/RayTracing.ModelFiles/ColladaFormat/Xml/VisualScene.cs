using System.Xml.Serialization;

namespace RayTracing.ModelFiles.ColladaFormat.Xml
{
    [XmlType("visual_scene")]
    public class VisualScene
    {
        [XmlAttribute("id")]
        public string? Id { get; set; }

        [XmlAttribute("name")]
        public string? Name { get; set; }

        [XmlElement("node")]
        public SceneNode[]? SceneNodes { get; set; }
    }
}
