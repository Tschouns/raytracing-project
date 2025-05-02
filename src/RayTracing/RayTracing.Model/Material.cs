namespace RayTracing.Model
{
    public class Material
    {
        public Material(
            string name,
            ArgbColor color,
            float reflectivity,
            float glossyness,
            float transparency,
            float indexOfRefraction)
        {
            this.Name = name;
            this.BaseColor = color;
            this.Reflectivity = reflectivity;
            this.Glossyness = glossyness;
            this.Transparency = transparency;
            this.IndexOfRefraction = indexOfRefraction;
        }

        public string Name { get; }
        public ArgbColor BaseColor { get; set; }
        public float Reflectivity { get; set; }
        public float Glossyness { get; set; }
        public float Transparency { get; set; }
        public float IndexOfRefraction { get; set; }
    }
}
