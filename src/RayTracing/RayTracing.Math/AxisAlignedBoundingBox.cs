using RayTracing.Math.Calculations;

namespace RayTracing.Math
{
    /// <summary>
    /// Represents an axis-aligned bounding box.
    /// </summary>
    public class AxisAlignedBoundingBox
    {
        private readonly Vector3 p000;
        private readonly Vector3 p00x;
        private readonly Vector3 p0y0;
        private readonly Vector3 p0yx;
        private readonly Vector3 pz00;
        private readonly Vector3 pz0x;
        private readonly Vector3 pzy0;
        private readonly Vector3 pzyx;

        public AxisAlignedBoundingBox(Vector3 min, Vector3 max)
        {
            if (min.X > max.X ||
                min.Y > max.Y ||
                min.Z > max.Z)
            {
                throw new ArgumentException($"The min ({min}) and max ({max}) arguments must contain the lowest and greatest values along all axes respectively.");
            }

            Min = min;
            Max = max;

            // Helper vectors:
            var v = this.Max - this.Min;
            var x = new Vector3(v.X, 0, 0);
            var y = new Vector3(0, v.Y, 0);
            var z = new Vector3(0, 0, v.Z);

            // Vertices:
            this.p000 = min;
            this.p00x = min + x;
            this.p0y0 = min + y;
            this.p0yx = min + y + x;
            this.pz00 = min + z;
            this.pz0x = min + z + x;
            this.pzy0 = min + z + y;
            this.pzyx = max;

            this.Edges = new Line3D[]
            {
                new Line3D(this.p000, this.p00x),
                new Line3D(this.p000, this.p0y0),
                new Line3D(this.p000, this.pz00),
                new Line3D(this.pzyx, this.pzy0),
                new Line3D(this.pzyx, this.pz0x),
                new Line3D(this.pzyx, this.p0yx),

                new Line3D(this.pz00, this.pzy0),
                new Line3D(this.pzy0, this.p0y0),
                new Line3D(this.p0y0, this.p0yx),
                new Line3D(this.p0yx, this.p00x),
                new Line3D(this.p00x, this.pz0x),
                new Line3D(this.pz0x, this.pz00),
            };

            this.Faces = new Triangle3D[]
            {
                new Triangle3D(this.p000, this.p00x, this.p0y0),
                new Triangle3D(this.p000, this.p0y0, this.pz00),

                new Triangle3D(this.p00x, this.p0yx, this.pzyx),
                new Triangle3D(this.p00x, this.pzyx, this.pz0x),

                new Triangle3D(this.p0yx, this.p0y0, this.pzy0),
                new Triangle3D(this.p0yx, this.pzy0, this.pzyx),

                new Triangle3D(this.p0y0, this.p000, this.pz00),
                new Triangle3D(this.p0y0, this.pz00, this.pzy0),

                new Triangle3D(this.p000, this.p0y0, this.p0yx),
                new Triangle3D(this.p000, this.p0yx, this.p00x),

                new Triangle3D(this.pz00, this.pz0x, this.pzyx),
                new Triangle3D(this.pz00, this.pzyx, this.pzy0),
            };
        }

        public Vector3 Min { get; }
        public Vector3 Max { get; }
        public IEnumerable<Line3D> Edges { get; }
        public IEnumerable<Triangle3D> Faces { get; }

        public bool Contains(Vector3 point)
        {
            return
                point.X >= this.Min.X && point.X <= this.Max.X &&
                point.Y >= this.Min.Y && point.Y <= this.Max.Y &&
                point.Z >= this.Min.Z && point.Z <= this.Max.Z;
        }

        public bool Contains(Vector3 point, float t)
        {
            return
                point.X > (this.Min.X - t) && point.X < (this.Max.X + t) &&
                point.Y > (this.Min.Y - t) && point.Y < (this.Max.Y + t) &&
                point.Z > (this.Min.Z - t) && point.Z < (this.Max.Z + t);
        }

        public bool Intersects(Triangle3D triangle)
        {
            // The box and triange intersect, if at least one cortner of the triangle is within the box.
            if (this.Contains(triangle.CornerA, 0.00001f) ||
                this.Contains(triangle.CornerB, 0.00001f) ||
                this.Contains(triangle.CornerC, 0.00001f))
            {
                return true;
            }

            // The box and triange intersect, if at least one box edge intersects the triangle.
            if (this.Edges.Any(edge => DoIntersect(edge, triangle)))
            {
                return true;
            }

            // The box and triange intersect, if at least one triangle edge intersects any of the box faces.
            if (this.Faces.Any(face =>
                DoIntersect(new Line3D(triangle.CornerA, triangle.CornerB), face) ||
                DoIntersect(new Line3D(triangle.CornerB, triangle.CornerC), face) ||
                DoIntersect(new Line3D(triangle.CornerC, triangle.CornerA), face)))
            {
                return true;
            }

            return false;
        }

        private static bool DoIntersect(Line3D line, Triangle3D triangle)
        {
            var check = VectorCalculator3D.IntersectTriangle(line, triangle);

            return
                check.HasIntersection &&
                check.Lambda >= -0.00001f &&
                check.Lambda <= 1.00001f;
        }
    }
}
