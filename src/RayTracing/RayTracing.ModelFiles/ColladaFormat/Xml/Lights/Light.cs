﻿using System.Xml.Serialization;

namespace RayTracing.ModelFiles.ColladaFormat.Xml.Lights
{
    [XmlType("light")]
    public class Light
    {
        [XmlAttribute("id")]
        public string? Id { get; set; }

        [XmlAttribute("name")]
        public string? Name { get; set; }

        [XmlElement("technique_common")]
        public LightTechniqueCommon? TechniqueCommon { get; set; }
    }
}
