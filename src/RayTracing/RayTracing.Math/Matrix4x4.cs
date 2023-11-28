
namespace RayTracing.Math
{
    /// <summary>
    /// Implements 4 x 4 matrix which can be used to represent transformations and homogeneous coordinates in 3D space.
    /// </summary>
    public struct Matrix4x4
    {
        #region ctors

        public Matrix4x4(
            float m00, float m01, float m02, float m03,
            float m10, float m11, float m12, float m13,
            float m20, float m21, float m22, float m23,
            float m30, float m31, float m32, float m33)
        {
            this.M00 = m00;
            this.M01 = m01;
            this.M02 = m02;
            this.M03 = m03;

            this.M10 = m10;
            this.M11 = m11;
            this.M12 = m12;
            this.M13 = m13;

            this.M20 = m20;
            this.M21 = m21;
            this.M22 = m22;
            this.M23 = m23;

            this.M30 = m30;
            this.M31 = m31;
            this.M32 = m32;
            this.M33 = m33;
        }

        #endregion

        #region properties

        public static Matrix4x4 Identity { get; } = new Matrix4x4(
            1, 0, 0, 0,
            0, 1, 0, 0,
            0, 0, 1, 0,
            0, 0, 0, 1);

        public float M00 { get; }
        public float M01 { get; }
        public float M02 { get; }
        public float M03 { get; }

        public float M10 { get; }
        public float M11 { get; }
        public float M12 { get; }
        public float M13 { get; }

        public float M20 { get; }
        public float M21 { get; }
        public float M22 { get; }
        public float M23 { get; }

        public float M30 { get; }
        public float M31 { get; }
        public float M32 { get; }
        public float M33 { get; }

        #endregion

        #region operations

        public Matrix4x4 Add(Matrix4x4 b)
        {
            return new Matrix4x4(
                    this.M00 + b.M00,
                    this.M01 + b.M01,
                    this.M02 + b.M02,
                    this.M03 + b.M03,

                    this.M10 + b.M10,
                    this.M11 + b.M11,
                    this.M12 + b.M12,
                    this.M13 + b.M13,

                    this.M20 + b.M20,
                    this.M21 + b.M21,
                    this.M22 + b.M22,
                    this.M23 + b.M23,

                    this.M30 + b.M30,
                    this.M31 + b.M31,
                    this.M32 + b.M32,
                    this.M33 + b.M33);
        }

        public Matrix4x4 Multiply(Matrix4x4 b)
        {
            return new Matrix4x4(
                // First row
                this.M00 * b.M00 + this.M01 * b.M10 + this.M02 * b.M20 + this.M03 * b.M30,
                this.M00 * b.M01 + this.M01 * b.M11 + this.M02 * b.M21 + this.M03 * b.M31,
                this.M00 * b.M02 + this.M01 * b.M12 + this.M02 * b.M22 + this.M03 * b.M32,
                this.M00 * b.M03 + this.M01 * b.M13 + this.M02 * b.M23 + this.M03 * b.M33,

                // Second row
                this.M10 * b.M00 + this.M11 * b.M10 + this.M12 * b.M20 + this.M13 * b.M30,
                this.M10 * b.M01 + this.M11 * b.M11 + this.M12 * b.M21 + this.M13 * b.M31,
                this.M10 * b.M02 + this.M11 * b.M12 + this.M12 * b.M22 + this.M13 * b.M32,
                this.M10 * b.M03 + this.M11 * b.M13 + this.M12 * b.M23 + this.M13 * b.M33,

                // Third row
                this.M20 * b.M00 + this.M21 * b.M10 + this.M22 * b.M20 + this.M23 * b.M30,
                this.M20 * b.M01 + this.M21 * b.M11 + this.M22 * b.M21 + this.M23 * b.M31,
                this.M20 * b.M02 + this.M21 * b.M12 + this.M22 * b.M22 + this.M23 * b.M32,
                this.M20 * b.M03 + this.M21 * b.M13 + this.M22 * b.M23 + this.M23 * b.M33,

                // Fourth row
                this.M30 * b.M00 + this.M31 * b.M10 + this.M32 * b.M20 + this.M33 * b.M30,
                this.M30 * b.M01 + this.M31 * b.M11 + this.M32 * b.M21 + this.M33 * b.M31,
                this.M30 * b.M02 + this.M31 * b.M12 + this.M32 * b.M22 + this.M33 * b.M32,
                this.M30 * b.M03 + this.M31 * b.M13 + this.M32 * b.M23 + this.M33 * b.M33);
        }

        #endregion

        #region transformations

        public static Matrix4x4 Translate(Vector3 t)
        {
            return new Matrix4x4(
                1, 0, 0, t.X,
                0, 1, 0, t.Y,
                0, 0, 1, t.Z,
                0, 0, 0, 1);
        }

        public static Matrix4x4 RotateX(float r)
        {
            var s = MathF.Sin(r);
            var c = MathF.Cos(r);

            return new Matrix4x4(
                1, 0, 0, 0,
                0, c, -s, 0,
                0, s, c, 0,
                0, 0, 0, 1);
        }

        public static Matrix4x4 RotateY(float r)
        {
            var s = MathF.Sin(r);
            var c = MathF.Cos(r);

            return new Matrix4x4(
                c, 0, s, 0,
                0, 1, 0, 0,
                -s, 0, c, 0,
                0, 0, 0, 1);
        }

        public static Matrix4x4 RotateZ(float r)
        {
            var s = MathF.Sin(r);
            var c = MathF.Cos(r);

            return new Matrix4x4(
                c, -s, 0, 0,
                s, c, 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1);
        }

        #endregion

        public Vector3 ApplyTo(Vector3 v)
        {
            var xw = M00 * v.X + M01 * v.Y + M02 * v.Z + M03;
            var yw = M10 * v.X + M11 * v.Y + M12 * v.Z + M13;
            var zw = M20 * v.X + M21 * v.Y + M22 * v.Z + M23;

            var w = M33;

            if (w == 1)
            {
                return new Vector3(
                    xw,
                    yw,
                    zw);
            }
            else
            {
                return new Vector3(
                    xw / w,
                    yw / w,
                    zw / w);
            }
        }

        public override string ToString()
        {
            return $"[\n[{M00}, {M01}, {M02}, {M03}],\n[{M10}, {M11}, {M12}, {M13}],\n[{M20}, {M21}, {M22}, {M23}],\n[{M30}, {M31}, {M32}, {M33}]]";
        }
    }
}
