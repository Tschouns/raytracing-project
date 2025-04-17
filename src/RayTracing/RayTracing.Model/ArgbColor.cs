namespace RayTracing.Model
{
    /// <summary>
    /// An color which represents ARGB values as scalar values between 0 and 1.
    /// </summary>
    public struct ArgbColor
    {
        public ArgbColor(float red, float green, float blue)
            : this(red, green, blue, 1f)
        {
        }

        public ArgbColor(float red, float green, float blue, float alpha)
        {
            this.A = Clamp(alpha);
            this.R = Clamp(red);
            this.G = Clamp(green);
            this.B = Clamp(blue);
        }

        public static ArgbColor Black => new ArgbColor(0f, 0f, 0f, 1f);

        public float R { get; }
        public float G { get; }
        public float B { get; }
        public float A { get; }

        public static ArgbColor operator +(ArgbColor a, ArgbColor b)
        {
            return new ArgbColor(
                a.R + b.R,
                a.G + b.G,
                a.B + b.B,
                a.A + b.A);
        }

        public static ArgbColor operator *(ArgbColor a, ArgbColor b)
        {
            return new ArgbColor(
                a.R * b.R,
                a.G * b.G,
                a.B * b.B,
                a.A * b.A);
        }

        public static ArgbColor operator *(ArgbColor color, float f)
        {
            return new ArgbColor(
                color.R * f,
                color.G * f,
                color.B * f,
                color.A * f);
        }

        private static float Clamp(float value)
        {
            return MathF.Min(MathF.Max(0f, value), 1f);
        }
    }
}
