
using RayTracing.Math;

namespace RayTracing.Rendering
{
    /// <summary>
    /// Represents a camera which is placed in space, and produces the rays.
    /// </summary>
    public class Camera : ICameraFixedProperties, ICameraSettings
    {
        private static readonly Vector2 defaultSize = new Vector2(0.008f, 0.006f);
        private static readonly ushort defaultHorizontalResolution = 800;
        private static readonly ushort defaultVerticalResolution = 600;
        private static readonly float defaultFocalLength = 0.01f;
        private static readonly Vector3 defaultPosition = new Vector3(0, 0, -10);
        private static readonly Vector3 defaultLookingDirection = new Vector3(0, 0, 1);

        public Camera()
        {
        }

        public Camera(
            ushort horizontalResolution,
            ushort verticalResolution)
        {
            HorizontalResolution = horizontalResolution;
            VerticalResolution = verticalResolution;

            var ratio = (float)horizontalResolution / VerticalResolution;
            this.Size = new Vector2 (
                defaultSize.Y * ratio,
                defaultSize.Y);

            this.FocalLength = defaultFocalLength;
        }

        public Camera(
            Vector2 size,
            ushort horizontalResolution,
            ushort verticalResolution,
            float focalLength,
            Vector3 position,
            Vector3 lookingDirection)
        {
            Size = size;
            HorizontalResolution = horizontalResolution;
            VerticalResolution = verticalResolution;

            FocalLength = focalLength;
            Position = position;
            LookingDirection = lookingDirection;
        }

        public Vector2 Size { get; } = defaultSize;
        public ushort HorizontalResolution { get; } = defaultHorizontalResolution;
        public ushort VerticalResolution { get; } = defaultVerticalResolution;

        public float FocalLength { get; set; } = defaultFocalLength;
        public Vector3 Position { get; set; } = defaultPosition;
        public Vector3 LookingDirection { get; set; } = defaultLookingDirection;

        /// <summary>
        /// Produces the rays, associated with pixel coordinates, based on the camera's settings.
        /// </summary>
        public IEnumerable<PixelRay> GetRasterRays()
        {
            throw new NotImplementedException();
        }
    }
}
