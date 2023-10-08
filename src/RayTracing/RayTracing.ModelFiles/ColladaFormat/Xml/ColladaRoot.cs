using System.Xml.Serialization;

namespace RayTracing.ModelFiles.ColladaFormat.Xml
{
    [XmlRoot("COLLADA", Namespace = "http://www.collada.org/2005/11/COLLADASchema", IsNullable = false)]
    public class ColladaRoot
    {
        [XmlArray("library_geometries")]
        public Geometry[]? LibraryGeometries { get; set; }

        [XmlArray("library_visual_scenes")]
        public VisualScene[]? LibraryVisualScenes { get; set; }
    }
}
