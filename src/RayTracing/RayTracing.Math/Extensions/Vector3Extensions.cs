namespace RayTracing.Math.Extensions
{
    public static class Vector3Extensions
    {
        public static Vector3 DirectionX(this Vector3 original)
        {
            return new Vector3(original.X, 0, 0);
        }

        public static Vector3 DirectionY(this Vector3 original)
        {
            return new Vector3(0, original.Y, 0);
        }

        public static Vector3 DirectionZ(this Vector3 original)
        {
            return new Vector3(0, 0, original.Z);
        }

        public static Vector3 MoveX(this Vector3 original, float x)
        {
            return new Vector3(x, original.Y, original.Z);
        }

        public static Vector3 MoveY(this Vector3 original, float y)
        {
            return new Vector3(original.X, y, original.Z);
        }

        public static Vector3 MoveZ(this Vector3 original, float z)
        {
            return new Vector3(original.X, original.Y, z);
        }
    }
}
