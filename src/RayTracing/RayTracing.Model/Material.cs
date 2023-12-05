
using System.Drawing;

namespace RayTracing.Model
{
    public class Material
    {
        public Material(
            string name,
            Color color)
        {
            Name = name;
            BaseColor = color;
        }

        public string Name { get; }
        public Color BaseColor { get; }
    }
}
