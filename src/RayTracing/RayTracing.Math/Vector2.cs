namespace RayTracing.Math
{
    public struct Vector2
    {
        public Vector2()
        {
            this.X = 0;
            this.Y = 0;
        }

        public Vector2(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        public float X { get; set; }
        public float Y { get; set; }

        public override string ToString()
        {
            return $"({this.X};{this.Y})";
        }

        public Vector2 Add(Vector2 b)
        {
            return new Vector2(
                this.X + b.X,
                this.Y + b.Y);
        }

        public Vector2 Invert()
        {
            return new Vector2(
                -this.X,
                -this.Y);
        }

        public Vector2 Scale(float c)
        {
            return new Vector2(
                this.X * c,
                this.Y * c);
        }

        public float Length()
        {
            return (float)System.Math.Sqrt(this.LengthSquared());
        }

        public float LengthSquared()
        {
            return this.X * this.X + this.Y * this.Y;
        }

        public static Vector2 operator - (Vector2 v)
        {
            return v.Invert();
        }

        public static Vector2 operator + (Vector2 a, Vector2 b)
        {
            return a.Add(b);
        }

        public static Vector2 operator - (Vector2 a, Vector2 b)
        {
            return a.Add(-b);
        }

        public static Vector2 operator * (Vector2 v, float c)
        {
            return v.Scale(c);
        }

        public static Vector2 operator * (float c, Vector2 v)
        {
            return v.Scale(c);
        }
    }
}