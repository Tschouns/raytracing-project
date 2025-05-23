﻿using RayTracing.ModelFiles.ColladaFormat.Xml.Effects;
using RayTracing.ModelFiles.ColladaFormat.Xml.Geometries;
using RayTracing.ModelFiles.ColladaFormat.Xml.Lights;
using RayTracing.ModelFiles.ColladaFormat.Xml.Materials;
using System.Xml.Serialization;

namespace RayTracing.ModelFiles.ColladaFormat.Xml
{
    [XmlRoot("COLLADA", Namespace = "http://www.collada.org/2005/11/COLLADASchema", IsNullable = false)]
    public class ColladaRoot
    {
        [XmlArray("library_lights")]
        public Light[]? LibraryLights { get; set; }

        [XmlArray("library_effects")]
        public Effect[]? LibraryEffects { get; set; }

        [XmlArray("library_materials")]
        public Material[]? LibraryMaterials { get; set; }

        [XmlArray("library_geometries")]
        public Geometry[]? LibraryGeometries { get; set; }

        [XmlArray("library_visual_scenes")]
        public VisualScene[]? LibraryVisualScenes { get; set; }
    }
}
