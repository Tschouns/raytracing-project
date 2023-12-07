
using System.Drawing;

namespace RayTracing.Model
{
    public class Material
    {
        public Material(
            string name,
            Color color,
            float reflectivity,
            float transparency,
            float indexOfRefraction)
        {
            Name = name;
            BaseColor = color;
            Reflectivity = reflectivity;
            Transparency = transparency;
            IndexOfRefraction = indexOfRefraction;
        }

        public string Name { get; }
        public Color BaseColor { get; }
        public float Reflectivity { get; }
        public float Transparency { get; }
        public float IndexOfRefraction { get; }
    }
}
