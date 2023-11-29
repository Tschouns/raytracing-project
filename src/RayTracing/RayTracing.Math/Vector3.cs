
namespace RayTracing.Math
{
    /// <summary>
    /// Represents a vector in 3D space.
    /// </summary>
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

        public static Vector3 Right { get; } = new Vector3(1, 0, 0);
        public static Vector3 Up { get; } = new Vector3(0, 1, 0);
        public static Vector3 Forward { get; } = new Vector3(0, 0, 1);

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

        public float Dot(Vector3 b)
        {
            return this.X * b.X + this.Y * b.Y + this.Z * b.Z;
        }

        public Vector3 Cross(Vector3 b)
        {
            var a = this;

            var x = a.Y * b.Z - a.Z * b.Y;
            var y = a.Z * b.X - a.X * b.Z;
            var z = a.X * b.Y - a.Y * b.X;

            return new Vector3(x, y, z);
        }

        public float Length()
        {
            return (float)System.Math.Sqrt(this.LengthSquared());
        }

        public float LengthSquared()
        {
            return 
                  (this.X * this.X)
                + (this.Y * this.Y)
                + (this.Z * this.Z);
        }

        public Vector3? Norm()
        {
            var length = this.Length();
            if (length == 0)
            {
                return null;
            }

            return this.Scale(1 / length);
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

        public static Vector3 operator /(Vector3 v, float c)
        {
            return v.Scale(1/c);
        }
    }
}
