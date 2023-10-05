
namespace RayTracing.Math
{
    public struct Vector3
    {
        public Vector3()
        {
            this.X = 0;
            this.Y = 0;
            this.Z = 0;
        }

        public Vector3(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public override string ToString()
        {
            return $"{this.X};{this.Y};{this.Z}";
        }

        public Vector3 Add(Vector3 b)
        {
            return new Vector3(
                this.X + b.X,
                this.Y + b.Y,
                this.Z + b.Z);
        }

        public Vector3 Invert()
        {
            return new Vector3(
                -this.X,
                -this.Y,
                -this.Z);
        }

        public Vector3 Scale(float c)
        {
            return new Vector3(
                this.X * c,
                this.Y * c,
                this.Z * c);
        }

        public float Length()
        {
            return (float)System.Math.Sqrt(this.LengthSquared());
        }

        public float LengthSquared()
        {
            return (this.X * this.X) + (this.Y * this.Y) + (this.Z * this.Z);
        }

        public Vector3 Norm()
        {
            return this.Scale(1 / this.Length());
        }

        public static Vector3 operator -(Vector3 v)
        {
            return v.Invert();
        }

        public static Vector3 operator +(Vector3 a, Vector3 b)
        {
            return a.Add(b);
        }

        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            return a.Add(-b);
        }

        public static Vector3 operator *(Vector3 v, float c)
        {
            return v.Scale(c);
        }

        public static Vector3 operator *(float c, Vector3 v)
        {
            return v.Scale(c);
        }
    }
}
