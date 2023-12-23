using RayTracing.Math;

namespace RayTracing.Model.BoundingBoxes
{
    /// <summary>
    /// Represents an axis-aligned bounding box.
    /// </summary>
    public class AxisAlignedBoundingBox
    {
        public AxisAlignedBoundingBox(Vector3 min, Vector3 max)
        {
            Min = min;
            Max = max;
        }

        public Vector3 Min;
        public Vector3 Max;

        public AxisAlignedBoundingBox CopyMove(Vector3 move)
        {
            return new AxisAlignedBoundingBox(Min + move, Max + move);
        }

        public bool Contains(Vector3 vertex)
        {
            if (vertex.X < Min.X || vertex.X > Max.X)
            {
                return false;
            }

            if (vertex.Y < Min.Y || vertex.Y > Max.Y)
            {
                return false;
            }

            if (vertex.Z < Min.Z || vertex.Z > Max.Z)
            {
                return false;
            }

            return true;
        }

        public override string ToString()
        {
            return $"({Min}) -> ({Max})";
        }
    }
}
