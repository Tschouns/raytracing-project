
namespace RayTracing.Math
{
    /// <summary>
    /// Represents a vector in 3D space.
    /// </summary>
    public struct Vector3
    {
        public Vector3()
        {
            X = 0;
            Y = 0;
            Z = 0;
        }

        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public override string ToString()
        {
            return $"{X};{Y};{Z}";
        }

        public static Vector3 Right { get; } = new Vector3(1, 0, 0);
        public static Vector3 Up { get; } = new Vector3(0, 1, 0);
        public static Vector3 Forward { get; } = new Vector3(0, 0, 1);

        public Vector3 Add(Vector3 b)
        {
            return new Vector3(
                X + b.X,
                Y + b.Y,
                Z + b.Z);
        }

        public Vector3 Invert()
        {
            return new Vector3(
                -X,
                -Y,
                -Z);
        }

        public Vector3 Scale(float c)
        {
            return new Vector3(
                X * c,
                Y * c,
                Z * c);
        }

        public Vector3 Scale(Vector3 c)
        {
            return new Vector3(
                X * c.X,
                Y * c.Y,
                Z * c.Z);
        }

        public float Dot(Vector3 b)
        {
            return X * b.X + Y * b.Y + Z * b.Z;
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
            return (float)System.Math.Sqrt(LengthSquared());
        }

        public float LengthSquared()
        {
            return
                  (X * X)
                + (Y * Y)
                + (Z * Z);
        }

        public Vector3? Norm()
        {
            var length = Length();
            if (length == 0)
            {
                return null;
            }

            return Scale(1 / length);
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
            return v.Scale(1 / c);
        }
    }
}
